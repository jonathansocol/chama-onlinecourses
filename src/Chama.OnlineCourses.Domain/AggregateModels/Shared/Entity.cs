﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.OnlineCourses.Domain.AggregateModels.Shared
{
    public class Entity<T>
    {
        public T Id { get; set; }
    }
}
