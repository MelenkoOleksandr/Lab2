﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabDBEntity.DAL
{
    public class TeacherCourse
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
