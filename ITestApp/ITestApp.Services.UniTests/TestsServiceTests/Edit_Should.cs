using ITestApp.Common.Exceptions;
using ITestApp.Common.Providers;
using ITestApp.Data.Models;
using ITestApp.Data.Repository;
using ITestApp.Data.Saver;
using ITestApp.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITestApp.Services.UnitTests.TestsServiceTests
{
    [TestClass]
    public class Edit_Should
    {
        [TestMethod]
        public void ThrowInvalidTestException_WhenNullTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            //Act & Assert
            Assert.ThrowsException<InvalidTestException>(() => testsService.Edit(null));
        }

        [TestMethod]
        public void ThrowInvalidTestException_WhenValidTestDtoIsProvided_ButTestIsNotFoundInDb()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 2
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);

            //Act & Assert
            Assert.ThrowsException<InvalidTestException>(() => testsService.Edit(testDto));
        }

        [TestMethod]
        public void ThrowInvalidTestExceptionWithExpectedMessage_WhenValidTestDtoIsProvided_ButTestIsNotFoundInDb()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 2
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            string expectedMsg = $"Test with id {testDto.Id} not found!";
            string actualMsg = "";

            //Act
            try
            {
                testsService.Edit(testDto);
            }
            catch (InvalidTestException ex)
            {
                actualMsg = ex.Message;
            }

            //Assert
            Assert.AreEqual(expectedMsg, actualMsg);
        }

        [TestMethod]
        public void CallTestRepositoryAllMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            //Act
            testsService.Edit(testDto);

            //Assert
            testsMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void ThrowInvalidTestException_WhenValidTestDtoIsProvidedButItsIdIsNotFoundInDb()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var testsService = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 2
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act & Assert
            Assert.ThrowsException<InvalidTestException>(() => testsService.Edit(testDto));
        }

        [TestMethod]
        public void CallQuestionRepositoryAllMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            questionsMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void CallAnswerRepositoryAllMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            answersMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void EditTestEntityTitleCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Title = "New title",
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Title = "Old title",
                    Id = 1
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.AreEqual(testDto.Title, testToEdit[0].Title);
        }

        [TestMethod]
        public void EditTestEntityCategoryIdCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                CategoryId = 1,
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    CategoryId = 2,
                    Id = 1
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.AreEqual(testDto.CategoryId, testToEdit[0].CategoryId);
        }

        [TestMethod]
        public void EditTestEntityRequiredTimeCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                RequiredTime = 10,
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    RequiredTime = 100,
                    Id = 1
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.RequiredTime == testToEdit[0].RequiredTime && testToEdit[0].RequiredTime == 10);
        }

        [TestMethod]
        public void EditTestEntityStatusIdCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                StatusId = 1,
                Id = 1,
                Questions = new List<QuestionDto>()
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    StatusId = 2,
                    Id = 1
                }
            };

            var questions = new List<Question>()
            {
                new Question
                {
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questions.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.StatusId == testToEdit[0].StatusId && testToEdit[0].StatusId == 1);
        }

        [TestMethod]
        public void EditQuestionEntityContentCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 1,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    StatusId = 2,
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Content = "Old content",
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.Questions.First().Content == testToEdit[0].Questions.First().Content && testToEdit[0].Questions.First().Content == "New content");
        }

        [TestMethod]
        public void EditAnswerEntityContentCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 1,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Content = "New content"
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Content = "Old content",
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Content = "Old content"
                        }
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.Questions.First().Answers.First().Content == testToEdit[0].Questions.First().Answers.First().Content && testToEdit[0].Questions.First().Answers.First().Content == "New content");
        }

        [TestMethod]
        public void EditAnswerEntityIsCorrectPropertyCorrectly_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 1,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Content = "New content",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Content = "Old content",
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Content = "Old content",
                            IsCorrect = false
                        }
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.Questions.First().Answers.First().IsCorrect == testToEdit[0].Questions.First().Answers.First().IsCorrect && testToEdit[0].Questions.First().Answers.First().IsCorrect == true);
        }

        [TestMethod]
        public void CallQuestionRepositoryUpdateMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 1,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Content = "New content",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Content = "Old content",
                    TestId = 1,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                        {
                            Content = "Old content",
                            IsCorrect = false
                        }
                    }
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            questionsMock.Verify(x => x.Update(It.Is<Question>(q => q.Id == questionsToEdit[0].Id)), Times.Once);
        }

        [TestMethod]
        public void CallAnswerRepositoryUpdateMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 123,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Id = 321,
                                Content = "New content",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 123,
                    Content = "Old content",
                    TestId = 1
                }
            };

            var answerToEdit = new List<Answer>()
            {
                new Answer()
                {
                    Id = 321,
                    QuestionId = 123,
                    Content = "Old content",
                    IsCorrect = false
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);
            answersMock.Setup(x => x.All).Returns(answerToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            answersMock.Verify(x => x.Update(It.Is<Answer>(a => a.Id == questionsToEdit[0].Answers.First().Id)), Times.Once);
        }

        [TestMethod]
        public void CallQuestionRepositoryDeleteMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 123,
                        Content = "New content",
                        IsDeleted = true,
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Id = 321,
                                Content = "New content",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 123,
                    Content = "Old content",
                    TestId = 1
                }
            };

            var answerToEdit = new List<Answer>()
            {
                new Answer()
                {
                    Id = 321,
                    QuestionId = 123,
                    Content = "Old content",
                    IsCorrect = false
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);
            answersMock.Setup(x => x.All).Returns(answerToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            questionsMock.Verify(x => x.Delete(It.Is<Question>(q => q.Id == questionsToEdit[0].Id)), Times.Once);
        }

        [TestMethod]
        public void CallAnswerRepositoryDeleteMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 123,
                        Content = "New content",
                        IsDeleted = true,
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Id = 321,
                                IsDeleted = true,
                                Content = "New content",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 123,
                    Content = "Old content",
                    TestId = 1
                }
            };

            var answerToEdit = new List<Answer>()
            {
                new Answer()
                {
                    Id = 321,
                    QuestionId = 123,
                    Content = "Old content",
                    IsCorrect = false
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);
            answersMock.Setup(x => x.All).Returns(answerToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            answersMock.Verify(x => x.Delete(It.Is<Answer>(a => a.Id == questionsToEdit[0].Answers.First().Id)), Times.Once);
        }

        [TestMethod]
        public void UpdateExistingQuestionsAndAddNewOnesWithoutAffectingTheOldOnesSuccessfully_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 123,
                        Content = "New content of already added question",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Id = 321,
                                IsDeleted = true,
                                Content = "New content of already added answer",
                                IsCorrect = true
                            }
                        }
                    },
                    new QuestionDto()
                    {
                        Id = 2,
                        Content = "Content of newly added question",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Id = 2,
                                IsDeleted = true,
                                Content = "Content of newly added answer",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 123,
                    Content = "Old content",
                    TestId = 1
                }
            };

            var answerToEdit = new List<Answer>()
            {
                new Answer()
                {
                    Id = 321,
                    QuestionId = 123,
                    Content = "Old content",
                    IsCorrect = false
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);
            answersMock.Setup(x => x.All).Returns(answerToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.Questions.First().Content == testToEdit[0].Questions.First().Content);
            Assert.IsTrue(testToEdit[0].Questions.Count == 2);
            Assert.IsTrue(testToEdit[0].Questions.ElementAt(0).Content == "New content of already added question" && testToEdit[0].Questions.ElementAt(1).Content == "Content of newly added question");
        }

        [TestMethod]
        public void UpdateExistingAnswersAndAddNewOnesWithoutAffectingTheOldOnesSuccessfully_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 123,
                        Content = "New content of already added question",
                        Answers = new List<AnswerDto>()
                        {
                            new AnswerDto()
                            {
                                Id = 321,
                                Content = "New content of already added answer",
                                IsCorrect = true
                            },
                            new AnswerDto()
                            {
                                Id = 2,
                                Content = "Content of newly added answer",
                                IsCorrect = true
                            }
                        }
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 123,
                    Content = "Old content",
                    TestId = 1
                }
            };

            var answerToEdit = new List<Answer>()
            {
                new Answer()
                {
                    Id = 321,
                    QuestionId = 123,
                    Content = "Old content"
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);
            answersMock.Setup(x => x.All).Returns(answerToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            Assert.IsTrue(testDto.Questions.First().Answers.First().Content == testToEdit[0].Questions.First().Answers.First().Content);
            Assert.IsTrue(testToEdit[0].Questions.First().Answers.Count == 2);
            Assert.IsTrue(testToEdit[0].Questions.First().Answers.ElementAt(0).Content == "New content of already added answer" && testToEdit[0].Questions.First().Answers.ElementAt(1).Content == "Content of newly added answer");
        }

        [TestMethod]
        public void CallTestRepositoryUpdateMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 1,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Content = "Old content",
                    TestId = 1,
                    Answers = new List<Answer>()
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            testsMock.Verify(x => x.Update(It.Is<Test>(t => t.Id == testToEdit[0].Id)), Times.Once);
        }

        [TestMethod]
        public void CallSaverSaveChangesMethodOnce_WhenValidTestDtoIsProvided()
        {
            //Arrange
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var testsMock = new Mock<IRepository<Test>>();
            var questionsMock = new Mock<IRepository<Question>>();
            var answersMock = new Mock<IRepository<Answer>>();
            var userTestsMock = new Mock<IRepository<UserTest>>();

            var sut = new TestsService(saverMock.Object, mapperMock.Object, testsMock.Object, questionsMock.Object, answersMock.Object, userTestsMock.Object);

            var testDto = new TestDto()
            {
                Id = 1,
                Questions = new List<QuestionDto>()
                {
                    new QuestionDto()
                    {
                        Id = 1,
                        Content = "New content",
                        Answers = new List<AnswerDto>()
                    }
                }
            };

            var testToEdit = new List<Test>()
            {
                new Test
                {
                    Id = 1
                }
            };

            var questionsToEdit = new List<Question>()
            {
                new Question
                {
                    Id = 1,
                    Content = "Old content",
                    TestId = 1,
                    Answers = new List<Answer>()
                }
            };

            testsMock.Setup(x => x.All).Returns(testToEdit.AsQueryable);
            questionsMock.Setup(x => x.All).Returns(questionsToEdit.AsQueryable);

            //Act
            sut.Edit(testDto);

            //Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
