﻿--
-- Create Table    : 'UserType'   
-- UserTypeID      :  
-- Type            :  
--
--
-- Create Table    : 'UserType'   
-- UserTypeID      :  
-- Type            :  
--
--
-- Create Table    : 'UserType'   
-- UserTypeID      :  
-- Type            :  
--
--
-- Create Table    : 'UserType'   
-- UserTypeID      :  
-- Type            :  
--
CREATE TABLE UserType (
    UserTypeID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Type           NCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_UserType PRIMARY KEY CLUSTERED (UserTypeID))