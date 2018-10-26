--
-- Create Table    : 'Userr'   
-- UserID          :  
-- UserName        :  
-- Password        :  
--
CREATE TABLE Userr (
    UserID         BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    UserName       NCHAR(20) NOT NULL UNIQUE,
    Password       NCHAR(20) NOT NULL,
CONSTRAINT pk_Userr PRIMARY KEY CLUSTERED (UserID))