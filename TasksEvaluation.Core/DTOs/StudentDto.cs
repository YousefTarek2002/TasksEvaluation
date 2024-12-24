using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Mapper;

namespace TasksEvaluation.Core.DTOs
{
	public class StudentDto : BaseDto  , IMapFrom
    {
        [Required]
		public string FullName { get; set; }
		[Required]
		public string MobileNumber { get; set; }
		[Required]
		public string Email { get; set; }
		public string ProfilePic { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDto>().ReverseMap();
        }
    }
}
