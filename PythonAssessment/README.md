# DataScience and Machine Learning in Python -tarazhong
This repository is based on Python3, scitkit-learn, pandas, seaborn. With these libraries, a lot can already been done in Data Science and Machine Learning.

## Installation :
**Prerequisit**
- Install python3
- Install pip (python get-pip.py)
- Install virtualenv via pip ($ [sudo] pip install virtualenv)
  - Create environment : $  virtualenv (C:\full\path\python3) ENV
  - Activate the virtual environment (for windows) : $ \path\to\env\Scripts\activate
  - To desactivate the environment : $ \path\to\env\Scripts\deactivate

## Once your virtual environment is activated [(ENV) C:\pathToProject\]

 $ cd C:\pathToProject\

 $ sudo pip install -r requirements.txt

**How to use the script**

__Choose classifier with the saved file__

Nearest Neighbors with a chosen k = 5 on 64000 samples : job_assessment.py --csv-file *profil.csv* --nearest-neighbors *5*

Logistic Regression on 1300 samples : job_assessment.py --csv-file *profil.csv* --logistic-regression --limit-samples *1300*

Neareast Neighbors with the best k (between 1 and 9) and Logistic Regression on the same training/testing set : job_assessment.py  --csv-file *profil.csv* --nearest-neighbors-logistic-regression

__Options__

Limit samples : --limit-samples *1300* (need to be >155 to have accurate metrics)

Increasing Learning set impact : --learning-curve
*Study the impact of a growing training set on accuracy (corresponding to 1%, 10%, 20%, 40%, 60%, 80% and 100% of the original one) We will always be testing on the same test set*

Increasing Testing set impact : --testing-curve
*Study the impact of a growing testing set on accuracy (corresponding to 1%, 10%, 20%, 40%, 60%, 80% and 99% of the original one) on the accuracy  We will always be training on the same train set*

*For all classifier train set size is set by default 0.8% of total samples*
