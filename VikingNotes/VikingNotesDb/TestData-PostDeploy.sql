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

INSERT INTO [dbo].[Userr] ([UserID], [UserName], [Password], [EmailAdress], [UserTypeID], [StudyID], [StudentNumber], [AuthToken], [Salt]) VALUES (1, N'SvendTheMan', N'123456', N'Svenden@gmail.com', 1, 1, N'201110642', N's34242d2', N'asdadsads')
INSERT INTO [dbo].[Userr] ([UserID], [UserName], [Password], [EmailAdress], [UserTypeID], [StudyID], [StudentNumber], [AuthToken], [Salt]) VALUES (2, N'Jens', N'megetlangkode', N'L33tM4ch1ne@gmail.com', 2, 2, N'20150652', N's34242d2', N'asdadsads')
INSERT INTO [dbo].[Userr] ([UserID], [UserName], [Password], [EmailAdress], [UserTypeID], [StudyID], [StudentNumber], [AuthToken], [Salt]) VALUES (3, N'Kirstine', N'utroliglangkode', N'Admin@VikingNotes.dk', 3, 3, N'201211071', N's34242d2', N'asdadsads')

SET IDENTITY_INSERT [dbo].[Userr] OFF
SET IDENTITY_INSERT [dbo].[Quiz] ON

/*kategori 1 bolsk Algebra*/
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (3, 3, N'Paratviden i Kodning - C', N'En quiz med simpel viden i C og C releteret kodning', 1)
/*Ikke lavet*/
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (4, 1, N'Let Boolsk algebra quiz', N'En let quiz', 1)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (5, 1, N'Svær Boolsk algebra quiz', N'En svær quiz', 1)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (6, 3, N'Omfattende Boolsk algebra quiz', N'En omfattende quiz', 1)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (7, 3, N'Tabels med mere', N'En quiz omkring de tabel som indgår i Boolsk Algebra', 1)
/*Ikke lavet slut*/

/*kategori 2 Software Design*/
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (1, 3, N'statemachines', N'essentielle design regler for statemachines', 2)
/*Ikke lavet*/
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (8, 3, N'Observer pattern', N'essentielle design regler for observer pattern', 2)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (9, 3, N'Trådhåndtering - Tasks', N'Quiz om hvordan har man gør brug af tasks', 2)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (10, 3, N'', N'essentielle design regler for statemachines', 2)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (11, 3, N'statemachines', N'essentielle design regler for statemachines', 2)
/*Ikke lavet slut*/

/*kategori 3 Software test*/
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (2, 3, N'unit testing', N'hvordan man sikrer god unit testing', 3)
/*Ikke lavet*/
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (12, 3, N'Code coverage', N'Spørgsmål omkring hvordan CC virker', 3)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (13, 3, N'Continous integration', N'En quiz lavet til de første 3 lektioner af SWT', 3)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (14, 3, N'Test med Klasse diagrammer', N'Hvilke ting man skal huske når man tester gennem klasse diagram', 3)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (15, 3, N'En bedre unittest', N'Denne her test er bedre end den anden test', 3)
/*Ikke lavet slut*/

SET IDENTITY_INSERT [dbo].[Quiz] OFF
SET IDENTITY_INSERT [dbo].[Question] ON

/*Quiz 1 - stateMachines*/
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (1, N'hvilken af disse design mønstre er lettest at vedligeholde', 1, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (13, N'hvordan skal man implementere Statemachine Pattern', 1, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (14, N'Hvilket diagram beskriver bedst Statemachine pattern', 1, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (15, N'Hvad kalder man en Condition for at man bevæger sig til et nyt state korrekt?', 1, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (16, N'Findes der andre pattern end STM?', 1, 0, 0)

/*Quiz 2 Unittest*/
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (2, N'noget andet', 2, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (17, N'Hvad hedder Guden for software test?', 2, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (18, N'Hvad hedder udvidelsen man bruger til at teste kode i VS og Jenkins?', 2, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (19, N'hvad er "The Art of Unittest" en reference til?', 2, 0, 0)

/*Quiz 3 - Parat viden i C*/
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (3, N'Hvilket C udtryk for AND operation er korrekt', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (4, N'Hvem er grundlæggeren af LINUX?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (5, N'Hvordan Dynamisk allokerer man Memory?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (6, N'Hvem er guden over indlejret software udvikling(ISU)?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (7, N'Hvad kan kode uden destructors forsage?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (8, N'Hvilken funktion kan bedst håndtere eksempelvis 10 forgreninger i en variabel?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (9, N'Hvilken type skal din funktion have hvis du returner true/false?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (10, N'Hvilken af disse sprog er ikke C?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (11, N'Hvilken er korrekt?', 3, 0, 0)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID], [WrongCount], [CorrectCount]) VALUES (12, N'Hvad hedder studie baren på IHA?', 3, 0, 0)

SET IDENTITY_INSERT [dbo].[Question] OFF
SET IDENTITY_INSERT [dbo].[Answer] ON

/*QUIZ 1 Statemachines*/
/*Spørgsmål 1 bedst vedligeholdes*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (1, N'GoF State', 1, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (2, N'SwitchCase', 0, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (3, N'Det lig meget', 0, 1)

/*Spørgsmål 2 Implementerer STM pattern*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (39, N'Ved at gøre states til objekter', 1, 13)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (40, N'Switch Case', 0, 13)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (41, N'IF -ELSE', 0, 13)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (42, N'Do WHile', 0, 13)

/*Spørgsmål 3 Diagram for STM pattern*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (43, N'Entity Relationship diagram', 0, 14)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (44, N'STM diagram', 1, 14)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (45, N'User Case Diagram', 0, 14)

/*Spørgsmål 4 Term for condition i overgange*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (46, N'Trigger', 0, 15)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (47, N'Guard', 1, 15)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (48, N'on entry', 0, 15)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (49, N'on exit', 0, 15)

/*Spørgsmål 5 findes der flere end STM Pattern*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (50, N'Ja', 1, 15)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (51, N'Nej', 0, 15)

/*Quiz 2 Unittest*/
/*Spørgsmål 1*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (4, N'ja', 1, 2)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (5, N'nej', 0, 2)

/*Spørgsmål 2 SWT guden*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (52, N'Frank "SWT GUD" Bodholdt Jakobsen', 1, 17)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (53, N'Troels Fedder Jensen', 0, 17)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (54, N'Poul Ejnar Rovsing', 0, 17)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (55, N'Jesper Rosholm Tørresø', 0, 17)

/*Spørgsmål 3 Hvad hedder udvidelsen i SWT*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (56, N'Notepad++', 0, 18)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (57, N'QT creator', 0, 18)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (58, N'Addblocker', 0, 18)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (59, N'nuget', 1, 18)

/*Spørgsmål 4 The art of Unittest*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (60, N'The Art of The Triggering', 0, 19)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (61, N'The Art of Art', 0, 19)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (62, N'The Art of 1337', 0, 19)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (63, N'The Art of War', 1, 19)

/*QUIZ 3 Paratviden*/
/*Spørgsmål 3 AND operator*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (6, N'++', 0, 3)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (7, N'||', 0, 3)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (8, N'&&', 1, 3)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (9, N'==', 0, 3)

/*Spørgsmål 4 - Linux*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (64, N'Torvald Linus', 1, 4)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (65, N'Bill Gates', 0, 4)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (66, N'Steven Jobs', 0, 4)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (67, N'Inger Støjberg', 0,4)

/*Spørgsmål 5 - DYN ALLOC*/
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (10, N'Struct', 0, 5)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (11, N'Switch', 0, 5)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (12, N'New', 1, 5)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (13, N'if - else', 0,5)

/*Spørgsmål 6 - ISU*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (14, N'Troels Fedder Jensen', 0, 6)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (15, N'Søren "ISU GUD" Hansen', 1, 6)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (16, N'Henrik Olsen', 0, 6)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (17, N'Torben Gergsen', 0,6)

/*Spørgsmål 7 - kode uden destructors*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (18, N'Et selvmål', 0, 7)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (19, N'Strækt venstre orienterede feminister der ikke vil gå væk igen, selvom du har sagt undskyldt, mange gange...', 0, 7)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (20, N'Memory Leak', 1, 7)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (21, N'En kortslutning i din Hardware', 0,7)

/*Spørgsmål 8 - Forgrening af kode*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (22, N'IF-ELSE', 0, 8)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (23, N'MEMORY LEAK', 0, 8)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (24, N'DO - WHILE', 0, 8)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (25, N'SWITCH - CASE', 1,8)

/*Spørgsmål 9 - funktion returner true/false*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (26, N'Void myFunction()', 0, 9)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (27, N'int myFunction()', 0, 9)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (28, N'myFunction myFunction()', 0, 9)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (29, N'bool myFunction()', 1,9)

/*Spørgsmål 10 - Hvilken er ikke C*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (30, N'C', 0, 10)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (31, N'C++', 0, 10)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (32, N'JavaScript', 1, 10)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (33, N'C#', 0,10)

/*Spørgsmål 11 - Hvad er korrekt*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (34, N'Korrekt', 1, 11)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (35, N'Forkert', 0, 11)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (36, N'Ved ikke', 0, 11)

/*Spørgsmål 12 - Studie Bar IHA C*/ 
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (37, N'Katrines Kælder', 0, 12)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (38, N'Kemisk studie bar', 0, 12)


SET IDENTITY_INSERT [dbo].[Answer] OFF
SET IDENTITY_INSERT [dbo].[Rating] ON

INSERT INTO [dbo].[Rating] ([RatingID], [Rating], [Reason], [QuizID]) VALUES (1, 5, N'den er super god, føler jeg har læst alt op', 2)
INSERT INTO [dbo].[Rating] ([RatingID], [Rating], [Reason], [QuizID]) VALUES (2, 2, N'rimeligt meh, og spørgsmål er for lette', 2)

SET IDENTITY_INSERT [dbo].[Rating] OFF