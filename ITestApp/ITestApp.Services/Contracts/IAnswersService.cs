using ITestApp.DTO;
using System.Collections;
using System.Collections.Generic;

namespace ITestApp.Services.Contracts
{
    public interface IAnswersService
    {
        AnswerDto GetById(int id);

        void Edit(AnswerDto answer);
    }
}
