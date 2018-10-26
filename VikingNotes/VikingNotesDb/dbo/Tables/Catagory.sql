--
-- Create Table    : 'Catagory'   
-- CatagoryID      :  
-- Name            :  
--
--
-- Create Table    : 'Catagory'   
-- CatagoryID      :  
-- Name            :  
--
CREATE TABLE Catagory (
    CatagoryID     BIGINT IDENTITY(1,1) NOT NULL UNIQUE,
    Name           NCHAR(20) NOT NULL UNIQUE,
CONSTRAINT pk_Catagory PRIMARY KEY CLUSTERED (CatagoryID))