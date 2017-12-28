setwd("C:/Users/hassiba/Desktop")
data=read.csv("dataset.csv")

View(data)


library(dummies)
colonnesRep1<-dummy(data$ID_Reponse1)
colonnesRep2<-dummy(data$ID_Reponse2)
colonnesRep3<-dummy(data$ID_Reponse3)
colonnesRep4<-dummy(data$ID_Reponse4)
colonnesRep5<-dummy(data$ID_Reponse5)
colonnesRep6<-dummy(data$ID_Reponse6)
colonnesRep7<-dummy(data$ID_Reponse7)
colonnesRep8<-dummy(data$ID_Reponse8)
colonnesRep9<-dummy(data$ID_Reponse9)
colonnesRep10<-dummy(data$ID_Reponse10)



data <- data[,colnames(data)!="ID_Reponse1"]
data <- data[,colnames(data)!="ID_Reponse2"]
data <- data[,colnames(data)!="ID_Reponse3"]
data <- data[,colnames(data)!="ID_Reponse4"]
data <- data[,colnames(data)!="ID_Reponse5"]
data <- data[,colnames(data)!="ID_Reponse6"]
data <- data[,colnames(data)!="ID_Reponse7"]
data <- data[,colnames(data)!="ID_Reponse8"]
data <- data[,colnames(data)!="ID_Reponse9"]
data <- data[,colnames(data)!="ID_Reponse10"]


data <- data[,colnames(data)!="Is_Architect"]
data <- data[,colnames(data)!="Is_DP"]
data <- data[,colnames(data)!="Is_Commercial"]
data <- data[,colnames(data)!="Is_Rien"]

data<-cbind(data,colonnesRep1,colonnesRep2,colonnesRep3,colonnesRep4,colonnesRep5,colonnesRep6,colonnesRep7,colonnesRep8,colonnesRep9,colonnesRep10)


library(glmnet)
library(pROC)
library(rpart)
library(rpart.plot)

dataX<-data
dataX$Is_CP<-NULL ; dataX<-as.matrix(dataX); dataY<-data$Is_CP


#-------------------------------------regression logistique--------------------------------------


mod<-cv.glmnet(dataX,dataY,family="binomial") 

#plot l'erreur en fonction du log(lambda)
plot(mod)

prediction<-predict(mod, newx = dataX, s = "lambda.min")

#methode ROC
plot.roc(dataY,prediction,print.auc=TRUE)

auc(roc(dataY,prediction))



