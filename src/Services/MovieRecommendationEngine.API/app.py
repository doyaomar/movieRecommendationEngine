"""
This script runs the application using a development server.
It contains the definition of routes and views for the application.
"""
import pandas as pd
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from flask import Flask, request, jsonify

#Init App
app = Flask(__name__)

# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app

##########CONTENT BASED FILTERING

def combine_features(row):
    return row['keywords']+" "+row['cast']+" "+row['genres']+" "+row['director']

def load_content_recommendations(): 
    content_df = pd.read_csv("Data/movie_dataset.csv")
    features = ['keywords','cast','genres','director']
    #filling all NaNs with blank string
    for feature in features:
        content_df[feature] = content_df[feature].fillna('') 
    #applying combined_features() method over each rows of dataframe and storing the combined string in "combined_features" column    
    content_df["combined_features"] = content_df.apply(combine_features,axis=1) 
    print("content_df cached in memory")
    return content_df 

def combine_features(row):
    return row['keywords']+" "+row['cast']+" "+row['genres']+" "+row['director']

def get_title_from_index(index):
    if not content_df[content_df.index == index]["title"].empty:
        return content_df[content_df.index == index]["tmdbId"].values[0]
    else:
        return []

def get_index_from_title(title):
    if not content_df[content_df.title.str.upper() == title.upper()].empty:
        return content_df[content_df.title.str.upper() == title.upper()]["index"].values[0]
    else:
        return []

def get_similar_movies(movie_user_likes):
    #creating new CountVectorizer() object
    cv = CountVectorizer()
    #feeding combined strings(movie contents) to CountVectorizer() object
    count_matrix = cv.fit_transform(content_df["combined_features"])

    cosine_sim = cosine_similarity(count_matrix)
    movie_index = get_index_from_title(movie_user_likes)
    similar_movies = list(enumerate(cosine_sim[movie_index]))    
    return sorted(similar_movies,key=lambda x:x[1],reverse=True)[1:]

#df = cache.ram('df3',load_recommendations,None)
content_df = load_content_recommendations() 
print(content_df.head())

#@app.route('/movies/content/recommendation/<movie_user_likes>/<number_of_elements>',methods=['GET'])
@app.route('/movies/content/recommendation',methods=['GET'])
def get_content_recommendations():
    movie_user_likes = request.args.get('movie_user_likes')
    number_of_elements = request.args.get('number_of_elements')

    print(movie_user_likes)
    recommended_movies = []
    sorted_similar_movies = get_similar_movies(movie_user_likes)
    sorted_similar_movies = sorted_similar_movies [0:int(number_of_elements)]
    
    for element in sorted_similar_movies:
        recommended_movies.append(int(get_title_from_index(element[0])))
    
    return jsonify(recommended_movies)
##########CONTENT BASED FILTERING END


##########COLLAB BASED FILTERING

def ff():
    ratings = pd.read_csv('Data/ratings.csv')
    movies = pd.read_csv('Data/movies.csv')
    links  = pd.read_csv('Data/links.csv')
    ratings = pd.merge(movies,ratings).drop(['genres','timestamp'],axis=1)
    ratings = pd.merge(ratings,links).drop(['imdbId'],axis=1)
    userRatings = ratings.pivot_table(index=['userId'],columns=['tmdbId'],values='rating')
    userRatings = userRatings.dropna(thresh=10, axis=1).fillna(0,axis=1)
    corrMatrix = userRatings.corr(method='pearson')
    #corrMatrix.to_csv('Data/item_similarityId_df.csv')
    #corrMatrix.head(100)
    print("raings created")
    #print(ratings.shape)
    print(corrMatrix.head(100))

#dd = ff()

def load_collab_recommendations(): 
    item_similarity_df = pd.read_csv("Data/item_similarity_df.csv",index_col=0)
    print("item_similarity_df cached in memory")
    return item_similarity_df 

#item_similarity_df = cache.ram('item_similarity_df3',load_collab_recommendations,None)
item_similarity_df = load_collab_recommendations() 
print(item_similarity_df.head())


##helper method that doesn't recommend a movie if the user has already seen it
def check_seen(recommended_movie,watched_movies):
    for movie_id,movie in watched_movies.items():
        if recommended_movie == movie["title"]:
            return True
    return False

def get_similar_movies(movie_name,user_rating):
    try:
        similar_score = item_similarity_df[movie_name]*(user_rating-2.5)
        similar_movies = similar_score.sort_values(ascending=False)
    except:
        print("don't have movie in model")
        similar_movies = pd.Series([])
    
    return similar_movies

@app.route('/movies/collab/recommendation',methods=['POST'])
def get_recommendations():
    watched_movies = request.json['watched_movies']

    print(watched_movies)
    similar_movies = pd.DataFrame()

    for movie_id,movie in watched_movies.items():
        similar_movies = similar_movies.append(get_similar_movies(movie["title"],movie["rating"]),ignore_index=True)

    all_recommend = similar_movies.sum().sort_values(ascending=False)

    recommended_movies = []
    for movie,score in all_recommend.iteritems():
        if not check_seen(movie,watched_movies):
            recommended_movies.append(movie)    

    if len(recommended_movies) > 100:
        recommended_movies = recommended_movies[0:100]        

    return jsonify(recommended_movies)

##########COLLAB BASED FILTERING END

@app.route('/')
def hello():
    """Renders a sample page."""
    return "Hello World!"

#Run Server
if __name__ == '__main__':
    import os
    HOST = os.environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(os.environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
