--
-- Create Table    : 'IsPartOfMany'   
-- CourseID        :  (references Course.CourseID)
-- CatagoryID      :  (references Catagory.CatagoryID)
--
--
-- Create Table    : 'IsPartOfMany'   
-- CourseID        :  (references Course.CourseID)
-- CatagoryID      :  (references Catagory.CatagoryID)
--
CREATE TABLE IsPartOfMany (
    CourseID       BIGINT NOT NULL,
    CatagoryID     BIGINT NOT NULL,
CONSTRAINT pk_IsPartOfMany PRIMARY KEY CLUSTERED (CourseID,CatagoryID),
CONSTRAINT fk_IsPartOfMany FOREIGN KEY (CourseID)
    REFERENCES Course (CourseID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
CONSTRAINT fk_IsPartOfMany2 FOREIGN KEY (CatagoryID)
    REFERENCES Catagory (CatagoryID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)