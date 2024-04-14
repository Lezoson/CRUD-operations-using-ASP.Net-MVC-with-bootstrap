﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsMangement.Models;
using System.Collections.Generic;

namespace StudentsMangement.Data
{
    public class StudentDBContext : IdentityDbContext<ApplicationUser>
    {
        public StudentDBContext(DbContextOptions options) : base(options)
        {

        }
       
        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }

        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<CourseDetails> CourseDetails { get; set; }


    }
}
