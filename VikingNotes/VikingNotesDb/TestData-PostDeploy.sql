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

SET IDENTITY_INSERT [dbo].[Study] ON

INSERT INTO [dbo].[Study] ([StudyID], [Name]) VALUES (1, N'IKT Ingeniør')
INSERT INTO [dbo].[Study] ([StudyID], [Name]) VALUES (2, N'Elektronik Ingeniør')
INSERT INTO [dbo].[Study] ([StudyID], [Name]) VALUES (3, N'Psykologi')

SET IDENTITY_INSERT [dbo].[Study] OFF
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
SET IDENTITY_INSERT [dbo].[Catagory] ON

INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (1, 'boolsk algebra')
INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (2, 'Software design')
INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (3, 'software testning')
INSERT INTO [dbo].[Catagory] ([CatagoryID], [Name]) VALUES (4, 'noget med psykologi')

SET IDENTITY_INSERT [dbo].[Catagory] OFF
SET IDENTITY_INSERT [dbo].[Quiz] ON

INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (1, 3, N'statemachines', N'essentielle design regler for statemachines', 2)
INSERT INTO [dbo].[Quiz] ([QuizID], [UserID], [Name], [Description], [CatagoryID]) VALUES (2, 3, N'unit testing', N'hvordan man sikrer god unit testing', 3)

SET IDENTITY_INSERT [dbo].[Quiz] OFF
SET IDENTITY_INSERT [dbo].[Question] ON

INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID]) VALUES (1, N'hvilken af disse design mønstre er lettest at vedligeholde', 1)
INSERT INTO [dbo].[Question] ([QuestionID], [Question], [QuizID]) VALUES (2, N'noget andet', 2)

SET IDENTITY_INSERT [dbo].[Question] OFF
SET IDENTITY_INSERT [dbo].[Answer] ON

INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (1, N'GoF State', 1, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (2, N'SwitchCase', 0, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (3, N'Det lig meget', 0, 1)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (4, N'ja', 1, 2)
INSERT INTO [dbo].[Answer] ([AnswerID], [Answer], [IsCorrect], [QuestionID]) VALUES (5, N'nej', 0, 2)

SET IDENTITY_INSERT [dbo].[Answer] OFF