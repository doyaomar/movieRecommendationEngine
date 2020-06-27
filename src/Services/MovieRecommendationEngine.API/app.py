"""
This script runs the application using a development server.
It contains the definition of routes and views for the application.
"""
import pandas as pd
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.metrics.pairwise import cosine_similarity
from flask import Flask, request, jsonify
app = Flask(__name__)

# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app

#mine

def combine_features(row):
    return row['keywords']+" "+row['cast']+" "+row['genres']+" "+row['director']

def load_recommendations(): 
    df = pd.read_csv("Data/movie_dataset.csv")
    features = ['keywords','cast','genres','director']
    #filling all NaNs with blank string
    for feature in features:
        df[feature] = df[feature].fillna('') 
    #applying combined_features() method over each rows of dataframe and storing the combined string in "combined_features" column    
    df["combined_features"] = df.apply(combine_features,axis=1) 
    print("df cached in memory")
    return df 

def combine_features(row):
    return row['keywords']+" "+row['cast']+" "+row['genres']+" "+row['director']

def get_title_from_index(index):
    if not df[df.index == index]["title"].empty:
        return df[df.index == index]["title"].values[0]
    else:
        return []

def get_index_from_title(title):
    if not df[df.title.str.upper() == title.upper()].empty:
        return df[df.title.str.upper() == title.upper()]["index"].values[0]
    else:
        return []

def get_similar_movies(movie_user_likes):
    #creating new CountVectorizer() object
    cv = CountVectorizer()
    #feeding combined strings(movie contents) to CountVectorizer() object
    count_matrix = cv.fit_transform(df["combined_features"])

    cosine_sim = cosine_similarity(count_matrix)
    movie_index = get_index_from_title(movie_user_likes)
    similar_movies = list(enumerate(cosine_sim[movie_index]))    
    return sorted(similar_movies,key=lambda x:x[1],reverse=True)[1:]

#df = cache.ram('df3',load_recommendations,None)
df = load_recommendations() 
print(df.head())

#@app.route('/movies/content/recommendation/<movie_user_likes>/<number_of_elements>',methods=['GET'])
@app.route('/movies/content/recommendation',methods=['GET'])
def get_recommendations():
    movie_user_likes = request.args.get('movie_user_likes')
    number_of_elements = request.args.get('number_of_elements')

    print(movie_user_likes)
    recommended_movies = []
    sorted_similar_movies = get_similar_movies(movie_user_likes)
    sorted_similar_movies = sorted_similar_movies [0:int(number_of_elements)]
    
    for element in sorted_similar_movies:
        recommended_movies.append(get_title_from_index(element[0]))
    
    return jsonify(recommended_movies)

#end mine

@app.route('/')
def hello():
    """Renders a sample page."""
    return "Hello World!"

if __name__ == '__main__':
    import os
    HOST = os.environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(os.environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
