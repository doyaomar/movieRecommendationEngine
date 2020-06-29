
#import pandas as pd
#from sklearn.feature_extraction.text import CountVectorizer
#from sklearn.metrics.pairwise import cosine_similarity

#movies = pd.read_csv('Data/movies.csv')
#links  = pd.read_csv('Data/links.csv')
#ratings  = pd.read_csv('Data/ratings.csv')
#movie_dataset = pd.read_csv("Data/movie_dataset.csv")

#####DATA POCESSING FOR CONTENT BASED

#links = links.dropna()
#links['tmdbId'] = links['tmdbId'].astype(int)
#movie_dataset['tmdbId'] = movie_dataset['tmdbId'].astype(int)
#movie_dataset = movie_dataset.drop(['index','budget','homepage','original_language','overview','popularity','production_companies','production_countries','release_date','revenue','runtime','spoken_languages','status','tagline','vote_average','vote_count','crew'],axis=1)

#movies = pd.merge(movies,links)
#movie_dataset = pd.merge(movie_dataset,movies,on='tmdbId')
#print(movie_dataset.head())

#movie_dataset.to_csv('Data/new_movie_dataset.csv',index=False)

#####END DATA POCESSING FOR CONTENT BASED

#####DATA POCESSING FOR COLLAB BASED

#ratings = pd.merge(movies,ratings).drop(['genres','timestamp'],axis=1)
#userRatings = ratings.pivot_table(index=['userId'],columns=['movieId'],values='rating')
#userRatings = userRatings.dropna(thresh=10, axis=1).fillna(0,axis=1)

#corrMatrix = userRatings.corr(method='pearson')
#print(corrMatrix.head())

#corrMatrix.to_csv('Data/new_item_similarity_df.csv')

####END DATA POCESSING FOR COLLAB BASED