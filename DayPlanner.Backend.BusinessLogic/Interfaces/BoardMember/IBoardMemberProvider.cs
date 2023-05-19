using DayPlanner.Backend.ApiModels.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.BoardMember
{
    public interface IBoardMemberProvider
    {
        Task<List<BoardModel>> GetMemberBoards(int userId);
    }
}
