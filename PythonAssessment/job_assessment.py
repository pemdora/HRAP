# -*- coding: utf-8 -*-
"""
Job Classification
EVA Project
"""

import argparse
import logging
import time
import sys

from tqdm import tqdm
import pandas as pd
from sklearn.cluster import KMeans
from sklearn.linear_model import LogisticRegression
from sklearn import metrics, neighbors
from sklearn.model_selection import train_test_split
import matplotlib.pyplot as plt

import numpy as np
import pickle

# Limit samples has to be fixed to not ork with predict

''' Predict for a given profile '''
def train_predict(classifier_name, classifier,X_train, y_train) :
    # Do Training@
    t0 = time.time()
    classifier_result=classifier.fit(X_train, y_train)
    logger.info("Training done in %0.3fs" % (time.time() - t0))


    print("Prediction for the given profile")
    print(classifier.predict([[6, 7, 6, 8, 7, 8, 6, 6, 7]])) # predict a given example

''' Report Classifier accuracy '''
# Train and print metrics for a given classifier with training set and test test
# Return train accuracy score and test accuracy score
def classifier_train_test(classifier_name, classifier,X_train, X_test, y_train, y_test) :
    # Do Training@
    t0 = time.time()
    classifier_result=classifier.fit(X_train, y_train)
    logger.info("Training done in %0.3fs" % (time.time() - t0))

    # Do testing
    logger.info("Testing "+classifier_name)
    t0 = time.time()
    # predicted_validation = neigh.predict(X_validation)
    predicted_test = classifier.predict(X_test)

    # Print score produced by metrics.classification_report and metrics.accuracy_score
    logger.info("Testing  done in %0.3fs" % (time.time() - t0))
    print('Score on training : %f' % classifier.score(X_train, y_train))
    print('Score on testing : %f' % classifier.score(X_test, y_test))
    print(metrics.classification_report(y_test,predicted_test))

    return classifier.score(X_train, y_train),metrics.accuracy_score(y_test,predicted_test)

''' Report Learning impact on accuracy '''
# Split the initial train set into 10 test set (1%,10%,20%,40%,60%,80%,100%) of original training set
def learning_impact(classifier_name,classifier,X_train,y_train,X_test,y_test):
    samples_train_rate=[0.01,0.10,0.20,0.40,0.60,0.80,1.00]
    train_accuracy = [0,0,0,0,0,0,0]
    test_accuracy = [0,0,0,0,0,0,0]
    for i in range(0,7): # for i=0 to i=6
        print('\n'+classifier_name+' :')
        X_train_set, X_unused, y_train_set, y_unused = train_test_split(X_train, y_train, train_size=samples_train_rate[i],shuffle=False)
        logger.info("Train set size is {}".format(X_train_set.shape))
        logger.info("Train size is {} % from original train set".format(samples_train_rate[i]*100))

        # Do Training and Testing
        train_accuracy[i],test_accuracy[i] = classifier_train_test(classifier_name,classifier,X_train_set, X_test, y_train_set, y_test)
        print(classifier_name+' Test accuracy score :', test_accuracy[i])
    # Display Training curve
    samples_train=X_train_set.shape[0]*np.array(samples_train_rate)
    display_learning_curve(classifier_name+" Learning curves",samples_train,train_accuracy,test_accuracy)
    plt.show()

# Display graph for training and testing accuracy for a variable train size
def display_learning_curve(title,samples_train,train_accuracy,test_accuracy) :
    plt.figure()
    plt.title(title)
    plt.xlabel("Training set size")
    plt.ylabel("Accuracy")
    plt.grid()
    plt.plot(samples_train,train_accuracy,linewidth=2.5,label="Train accuracy ")
    plt.plot(samples_train,test_accuracy,linewidth=2.5,label="Test accuracy ")
    plt.legend(loc="best")
    return plt

''' Report Testing impact on accuracy '''
# Report Testig impact on accuracy
# Split the initial test set into 10 test set (1%,10%,20%,40%,60%,80%,99%) of original testing set
def testing_impact(classifier_name,classifier,X_train,y_train,X_test,y_test):
    samples_test_rate=[0.01,0.10,0.20,0.40,0.60,0.80,0.99]
    means=[]
    st_deviations=[]
    for i in range(0,7):
        train_accuracy = np.empty(10)
        test_accuracy = np.empty(10)
        for j in range(0,10): # Testing 10 different testing set on the same ratio split
            print('\n'+classifier_name+' :')
            X_unused, X_test_set, y_unused, y_test_set = train_test_split(X_test, y_test, test_size=samples_test_rate[i])
            logger.info("Test set size is {}".format(X_test_set.shape))
            logger.info("Test size is {} % from original test set".format(samples_test_rate[i]*100))

            # Do Training and Testing
            train_accuracy[j],test_accuracy[j] = classifier_train_test(classifier_name,classifier,X_train, X_test_set, y_train, y_test_set)
            print(classifier_name+' Test accuracy score :', test_accuracy[j])
        # Compute mean acurracy and standard deviation
        means.append(np.mean(test_accuracy))
        st_deviations.append(np.std(test_accuracy))
    # Display Testing curve
    samples_test=X_test_set.shape[0]*np.array(samples_test_rate)
    display_testing_curve(classifier_name+" Testing curves",samples_test,means,st_deviations)
    plt.show()


# Display graph for training and testing accuracy for a variable train size
def display_testing_curve(title,samples_test,mean_accuracy,std_mean_accuracy) :
    plt.figure()
    plt.title(title)
    plt.xlabel("Testing set size")
    plt.ylabel("Test Accuracy")
    plt.grid()
    mean = pd.DataFrame(mean_accuracy)
    errors = pd.DataFrame(std_mean_accuracy)
    plt.scatter(samples_test,mean_accuracy,s=50,label="Mean")
    plt.errorbar(samples_test,mean_accuracy, std_mean_accuracy, ecolor='green', ms=10, label="Standard deviation")
    plt.legend(loc="best")
    return plt



# Setup logging
logger = logging.getLogger('job_assessment.py')
logger.setLevel(logging.INFO)
ch = logging.StreamHandler()
ch.setFormatter(logging.Formatter('%(asctime)s - %(levelname)s - %(message)s'))
logger.addHandler(ch)

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Extract features, train a classifier on images and test the classifier')
    input_group = parser.add_mutually_exclusive_group(required=True)
    input_group.add_argument('--csv-file',help='file containing the people profile, one per line, comma separated')
    # input_group.add_argument('--load-features',help='read features and class from pickle file')
    # parser.add_argument('--save-features',help='save features in pickle format')
    result_group =  parser.add_mutually_exclusive_group(required=True)
    result_group.add_argument('--predict',action='store_true')
    result_group.add_argument('--accuracy',action='store_true')
    classifier_group = parser.add_mutually_exclusive_group(required=False)
    classifier_group.add_argument('--nearest-neighbors',type=int)
    classifier_group.add_argument('--nearest-neighbors-logistic-regression', action='store_true', help='train knn with 60% training, 20% validation select the best k for k=1 to k=9, 20% test then do logistic regression')
    classifier_group.add_argument('--logistic-regression', action='store_true')
    parser.add_argument('--limit-samples',type=int, help='limit the number of samples to consider for training')
    # classifier_group.add_argument('--features-only', action='store_true', help='only extract features, do not train classifiers')
    learning_testing_impact =  parser.add_mutually_exclusive_group(required=False)
    learning_testing_impact.add_argument('--learning-curve',action='store_true',help='study the impact of a growing training set [(1%,10%,20%,40%,60%,80%,100%) of original training set] on the accuracy')
    learning_testing_impact.add_argument('--testing-curve',action='store_true',help='study the impact of a growing testing set [(1%,10%,20%,40%,60%,80%,99%) of original training set] on the accuracy')
    args = parser.parse_args()

    """
    if args.load_features:
        # read features from to_pickle
        dataframe = pd.read_pickle(args.load_features)
        if args.limit_samples:
            dataframe = dataframe.sample(n=args.limit_samples)
        y = list(dataframe['class'])
        X = dataframe.drop(columns='class')
        pass
    else:
    """
    # Load data  from CSV file using pd.read_csv
    # "Personne,Leadership Sociabilité,Contrôle émotionnel,Atteinte des objectifs,Avant vente,Pilotage Suivi,Relation client,Gestion équipe,Reporting,Poste"
    file_list = []
    file_list = pd.read_csv(args.csv_file, header=None, names=['Personne','Leadership','Sociabilité','Contrôle émotionnel','Atteinte des objectifs','Avant vente','Pilotage Suivi','Relation client','Gestion équipe','Reporting','classe']) # return a DataFrame
    y = file_list['classe'] # class to predict
    data = []
    data =  file_list.drop(columns=['Personne','classe'])
    # print(data)
    # check that we have data
    if data.empty:
        logger.error("Could not extract any features or class")
        sys.exit(1)

    # convert to np.array
    X = np.array(data)

    '''
    # save features
    if args.save_features:
        # convert X to dataframe with pd.DataFrame and save to pickle with to_pickle
        dataframe = pd.DataFrame(X)
        dataframe['class'] = y
        dataframe.to_pickle(args.save_features + '.pkl')
        logger.info('Saved {} features and class to {}'.format(dataframe.shape,args.save_features))
    '''

    # Train classifier
    logger.info("Training Classifier")
    '''
    if args.features_only:
        logger.info('No classifier to train, exit')
        sys.exit()
    '''
    if args.nearest_neighbors:
        # Predict for a given profil
        if args.predict:
            # create KNN classifier with args.nearest_neighbors as a parameter
            neigh = neighbors.KNeighborsClassifier(n_neighbors=args.nearest_neighbors)
            logger.info('Use kNN classifier with k= {}'.format(args.nearest_neighbors))
            logger.info("Train set size is {}".format(X.shape))

            train_predict("KNN",neigh,X,y);
        # Print classifier accuracy
        else:
            # create KNN classifier with args.nearest_neighbors as a parameter
            neigh = neighbors.KNeighborsClassifier(n_neighbors=args.nearest_neighbors)
            logger.info('Use kNN classifier with k= {}'.format(args.nearest_neighbors))

            # Use train_test_split to create train and validation/test split
            X_train, X_test, y_train, y_test = train_test_split(X, y, train_size=0.8)
            logger.info("Train set size is {}".format(X_train.shape))
            logger.info("Test set size is {}".format(X_test.shape))

            # If we want to study Increasing training set impact
            if args.learning_curve:
                learning_impact("KNN",neigh,X_train,y_train,X_test,y_test);
            elif args.testing_curve:
                testing_impact("KNN",neigh,X_train,y_train,X_test,y_test)
            else:
                # Do Training and testing
                train_accuracy, test_accuracy = classifier_train_test("KNN",neigh,X_train, X_test, y_train, y_test)
                print('Knn Train accuracy score : ',train_accuracy)
                print('Knn Test accuracy score :', test_accuracy)


    elif args.logistic_regression:
        # Predict for a given profil
        if args.predict:
            # create KNN classifier with args.nearest_neighbors as a parameter
            logistic_reg = LogisticRegression()
            logger.info('Use logistic_regression classifier')
            logger.info("Train set size is {}".format(X.shape))

            train_predict("Logistic Regression",logistic_reg,X,y);
        # Print classifier accuracy
        else:
            # create logistic regression classifier
            logistic_reg = LogisticRegression()
            logger.info('Use logistic_regression classifier')

            # Use train_test_split to create train/test split
            X_train, X_test, y_train, y_test = train_test_split(X, y, train_size=0.8)
            logger.info("Train set size is {}".format(X_train.shape))
            logger.info("Test set size is {}".format(X_test.shape))

            # If we want to study Increasing training set impact
            if args.learning_curve:
                learning_impact("Logistic Regression",logistic_reg,X_train,y_train,X_test,y_test);
            elif args.testing_curve:
                testing_impact("Logistic Regression",logistic_reg,X_train,y_train,X_test,y_test)
            else:
                # Do Training and testing
                train_accuracy, test_accuracy = classifier_train_test("Logistic Regression",logistic_reg,X_train, X_test, y_train, y_test)
                print('Logistic Train accuracy score :', train_accuracy)
                print('Logistic Test accuracy score :', test_accuracy)


    elif args.nearest_neighbors_logistic_regression:
        # Use train_test_split to create train and validation/test split
        X_train_validation, X_test, y_train_validation, y_test = train_test_split(X, y, train_size=0.8)
        logger.info("Test set size is {}".format(X_test.shape))

        # Use train_test_split to create train/validation split
        X_train, X_validation, y_train, y_validation = train_test_split(X_train_validation, y_train_validation, train_size=0.8)
        logger.info("Training set size is {}".format(X_train.shape))
        logger.info("Validation set size is {}".format(X_validation.shape))

        # Select best KNN classifier k for k=0 to 9
        best_k = [0,0] # k, accuracy_score
        for i in range(1,10):
            neigh = neighbors.KNeighborsClassifier(n_neighbors=i)
            logger.info('Use kNN classifier with k= {}'.format(i))
            # Do Training and Testing
            train_accuracy, test_accuracy = classifier_train_test("KNN",neigh,X_train, X_validation, y_train, y_validation)
            print('Knn accuracy score :', test_accuracy)
            if (test_accuracy>best_k[1]):
                best_k[0] = i
                best_k[1] = test_accuracy

        # Exectute classifier
        print('Best k is = {}'.format(best_k[0]))
        print('Accuracy score from validation = {}'.format(best_k[1]))
        neigh = neighbors.KNeighborsClassifier(n_neighbors=best_k[0])
        logistic_reg = LogisticRegression()

        # Display Training curve
        if args.learning_curve:
            # Split the initial train set into 10 test set (1%,10%,20%,40%,60%,80%,100%) of original training set
            samples_train_rate=[0.01,0.10,0.20,0.40,0.60,0.80,1.00]
            train_accuracy_knn = [0,0,0,0,0,0,0]
            test_accuracy_knn = [0,0,0,0,0,0,0]
            train_accuracy_log = [0,0,0,0,0,0,0]
            test_accuracy_log = [0,0,0,0,0,0,0]
            for i in range(0,7): # for i=0 to i=6
                X_train_set, X_unused, y_train_set, y_unused = train_test_split(X_train_validation, y_train_validation, train_size=samples_train_rate[i],shuffle=False)
                logger.info("Train set size is {}".format(X_train_set.shape))
                logger.info("Train size is {} % from original train set".format(samples_train_rate[i]*100))

                # Do Training and Testing
                print('Knn :')
                train_accuracy_knn[i],test_accuracy_knn[i] = classifier_train_test("KNN",neigh,X_train_set, X_test, y_train_set, y_test)
                print('Logistic Regression :')
                train_accuracy_log[i],test_accuracy_log[i] = classifier_train_test("Logistic Regression",logistic_reg,X_train_set, X_test, y_train_set, y_test)
                print('Knn Test accuracy score :', test_accuracy_knn[i])
                print('Logistic Test accuracy score :', test_accuracy_log[i])

            samples_train=X_train_set.shape[0]*np.array(samples_train_rate)
            display_learning_curve("Nearest Neighbors Learning curves",samples_train,train_accuracy_knn,test_accuracy_knn)
            display_learning_curve("Logistic Regression Learning curves",samples_train,train_accuracy_log,test_accuracy_log)
            plt.show()
        elif args.testing_curve:
            samples_test_rate=[0.01,0.10,0.20,0.40,0.60,0.80,0.99]
            means_knn=[]
            st_deviations_knn=[]
            means_log=[]
            st_deviations_log=[]
            for i in range(0,7):
                train_accuracy_knn = np.empty(10)
                test_accuracy_knn = np.empty(10)
                train_accuracy_log = np.empty(10)
                test_accuracy_log = np.empty(10)
                for j in range(0,10): # Testing 10 different testing set on the same ratio split
                    X_unused, X_test_set, y_unused, y_test_set = train_test_split(X_test, y_test, test_size=samples_test_rate[i])
                    logger.info("Test set size is {}".format(X_test_set.shape))
                    logger.info("Test size is {} % from original test set".format(samples_test_rate[i]*100))

                    # Do Training and Testing
                    train_accuracy_knn[j],test_accuracy_knn[j] = classifier_train_test("KNN",neigh,X_train, X_test_set, y_train, y_test_set)
                    train_accuracy_log[j],test_accuracy_log[j] = classifier_train_test("Logistic Regression",logistic_reg,X_train, X_test_set, y_train, y_test_set)
                # Compute mean acurracy and standard deviation
                means_knn.append(np.mean(test_accuracy_knn))
                st_deviations_knn.append(np.std(test_accuracy_knn))
                means_log.append(np.mean(test_accuracy_log))
                st_deviations_log.append(np.std(test_accuracy_log))
            # Display Testing curve
            samples_test=X_test_set.shape[0]*np.array(samples_test_rate)
            display_testing_curve("KNN"+" Testing curves",samples_test,means_knn,st_deviations_knn)
            display_testing_curve("Logistic Regression"+" Testing curves",samples_test,means_log,st_deviations_log)
            plt.show()

        else:
            # Do Training and testing
            print('Knn :')
            train_accuracy_knn,test_accuracy_knn = classifier_train_test("KNN",neigh,X_train_validation, X_test, y_train_validation, y_test)
            print('Logistic Regression :')
            train_accuracy_log,test_accuracy_log = classifier_train_test("Logistic Regression",logistic_reg,X_train_validation, X_test, y_train_validation, y_test)
            print('Knn Test accuracy score :', test_accuracy_knn)
            print('Logistic Test accuracy score :', test_accuracy_log)

    else:
        logger.error('No classifier specified')
        sys.exit()
