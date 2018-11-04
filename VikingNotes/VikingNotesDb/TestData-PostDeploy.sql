/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[Faculty] ON

INSERT INTO [dbo].[Faculty] ([FacultyID], [Name]) VALUES (1, N'Science And Technology')
INSERT INTO [dbo].[Faculty] ([FacultyID], [Name]) VALUES (2, N'Arts')
INSERT INTO [dbo].[Faculty] ([FacultyID], [Name]) VALUES (3, N'Health')
INSERT INTO [dbo].[Faculty] ([FacultyID], [Name]) VALUES (4, N'Aarhus BSS')

SET IDENTITY_INSERT [dbo].[Faculty] OFF
SET IDENTITY_INSERT [dbo].[Study] ON

INSERT INTO [dbo].[Study] ([StudyID], [Name], [FacultyID]) VALUES (1, N'IKT Ingeniør', 1)
INSERT INTO [dbo].[Study] ([StudyID], [Name], [FacultyID]) VALUES (2, N'Elektronik Ingeniør', 1)
INSERT INTO [dbo].[Study] ([StudyID], [Name], [FacultyID]) VALUES (3, N'Psykologi', 4)

SET IDENTITY_INSERT [dbo].[Study] OFF
SET IDENTITY_INSERT [dbo].[Semester] ON

INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (1, N'1', 1)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (2, N'2', 1)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (3, N'3', 1)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (4, N'4', 1)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (6, N'6+7', 1)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (7, N'1', 2)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (8, N'2', 2)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (9, N'3', 2)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (10, N'4', 2)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (11, N'6+7', 2)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (12, N'1', 3)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (13, N'2', 3)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (14, N'3', 3)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (15, N'4', 3)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (16, N'5', 3)
INSERT INTO [dbo].[Semester] ([SemesterID], [SemesterNumber], [StudyID]) VALUES (17, N'6', 3)

SET IDENTITY_INSERT [dbo].[Semester] OFF
SET IDENTITY_INSERT [dbo].[Course] ON

INSERT INTO [dbo].[Course] ([CourseID], [Name], [SemesterID]) VALUES (1, N'I4SWD', 4)
INSERT INTO [dbo].[Course] ([CourseID], [Name], [SemesterID]) VALUES (2, N'I4SWT', 4)
INSERT INTO [dbo].[Course] ([CourseID], [Name], [SemesterID]) VALUES (3, N'E1IDE', 7)
INSERT INTO [dbo].[Course] ([CourseID], [Name], [SemesterID]) VALUES (4, N'Psyk101', 12)

SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[Catagory] ON

INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (1, 'boolsk algebra')
INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (2, 'Software design')
INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (3, 'software testning')
INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (4, 'noget med psykologi')

SET IDENTITY_INSERT [dbo].[Catagory] OFF

INSERT INTO [dbo].[IsPartOfMany] ([CatagoryID], [CourseID]) VALUES (1, 3)
INSERT INTO [dbo].[IsPartOfMany] ([CatagoryID], [CourseID]) VALUES (2, 1)
INSERT INTO [dbo].[IsPartOfMany] ([CatagoryID], [CourseID]) VALUES (3, 2)
INSERT INTO [dbo].[IsPartOfMany] ([CatagoryID], [CourseID]) VALUES (4, 4)

SET IDENTITY_INSERT [dbo].[UserType] ON

INSERT INTO [dbo].[UserType] ([UserTypeID], [Type]) VALUES (1, N'AlmindeligBruger')
INSERT INTO [dbo].[UserType] ([UserTypeID], [Type]) VALUES (2, N'StudieAdmin')
INSERT INTO [dbo].[UserType] ([UserTypeID], [Type]) VALUES (3, N'SystemAdmin')


SET IDENTITY_INSERT [dbo].[UserType] OFF
SET IDENTITY_INSERT [dbo].[Userr] ON

INSERT INTO [dbo].[Userr] ([UserID], [UserName], [Password], [EmailAdress], [UserTypeID], [StudyID], [StudentNumber]) VALUES (1, N'SvendTheMan', N'123456', N'Svenden@gmail.com', 1, 3, N'201110642')
INSERT INTO [dbo].[Userr] ([UserID], [UserName], [Password], [EmailAdress], [UserTypeID], [StudyID], [StudentNumber]) VALUES (2, N'Jens', N'megetlangkode', N'L33tM4ch1ne@gmail.com', 2, 2, N'20150652')
INSERT INTO [dbo].[Userr] ([UserID], [UserName], [Password], [EmailAdress], [UserTypeID], [StudyID], [StudentNumber]) VALUES (3, N'Kirstine', N'utroliglangkode', N'Admin@VikingNotes.dk', 3, 3, N'201211071')

SET IDENTITY_INSERT [dbo].[Userr] OFF
SET IDENTITY_INSERT [dbo].[Quiz] ON

INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (1, 3, N'statemachines', N'essentielle design regler for statemachines', 2)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (2, 3, N'unit testing', N'hvordan man sikrer god unit testing', 3)

SET IDENTITY_INSERT [dbo].[Quiz] OFF
SET IDENTITY_INSERT [dbo].[Question] ON

INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (1, N'hvilken af disse design mønstre er lettest at vedligeholde', 1, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (2, N'noget andet', 2, 0, 0)

SET IDENTITY_INSERT [dbo].[Question] OFF
SET IDENTITY_INSERT [dbo].[Answer] ON

INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (1, N'GoF State', 1, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (2, N'SwitchCase', 0, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (3, N'Det lig meget', 0, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (4, N'ja', 1, 2)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (5, N'nej', 0, 2)

SET IDENTITY_INSERT [dbo].[Answer] OFF
SET IDENTITY_INSERT [dbo].[Rating] ON

INSERT INTO [dbo].[Rating] ([RatingID], [Rating], [Reason], [QuizID]) VALUES (1, 5, N'den er super god, føler jeg har læst alt op', 2)
INSERT INTO [dbo].[Rating] ([RatingID], [Rating], [Reason], [QuizID]) VALUES (2, 2, N'rimeligt meh, og spørgsmål er for lette', 2)

SET IDENTITY_INSERT [dbo].[Rating] OFF