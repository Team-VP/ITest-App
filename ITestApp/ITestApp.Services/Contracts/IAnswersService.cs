using ITestApp.DTO;
using System.Collections;
using System.Collections.Generic;

namespace ITestApp.Services.Contracts
{
    public interface IAnswersService
    {
        //void Edit(AnswerDto answer);

        //void Delete(int id);

        AnswerDto GetById(int id);
    }
}
