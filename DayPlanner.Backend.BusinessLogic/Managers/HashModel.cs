﻿namespace DayPlanner.Backend.BusinessLogic.Managers
{
    public class HashModel
    {
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
    }
}