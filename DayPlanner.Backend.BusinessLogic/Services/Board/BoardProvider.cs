﻿using AutoMapper;
using DayPlanner.Backend.ApiModels.Board;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;


namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class BoardProvider : IBoardProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        //private readonly IUserContextService _userContextService;
        public BoardProvider(DataContext context,
            IMapper mapper) //IUserContextService userContextService
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BoardModel>> GetBoards()
        {
            var boards = await _context.Boards
            .OrderBy(t => t.Id)
                .ToListAsync();

            var boardModels = _mapper.Map<List<BoardModel>>(boards);

            return boardModels;
        }
        public async Task<BoardModel> GetBoard(int boardId)
        {
            var board = await _context.Boards
                .Where(b => b.Id == boardId)
                .FirstOrDefaultAsync();

            var boardModel = _mapper.Map<BoardModel>(board);

            return boardModel;
        }

        
    }
}
