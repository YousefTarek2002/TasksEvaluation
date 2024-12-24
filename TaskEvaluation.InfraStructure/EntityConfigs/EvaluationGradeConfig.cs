﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.Business;

namespace TaskEvaluation.InfraStructure.EntityConfigs
{
	public class EvaluationGradeConfig : IEntityTypeConfiguration<EvaluationGrade>
	{
		public void Configure(EntityTypeBuilder<EvaluationGrade> builder)
		{
			builder.Property(eg => eg.Grade).IsRequired();

			// one EvaluationGrade Has one Solution
			builder.HasOne(eg => eg.Solution)
				.WithOne(s => s.EvaluationGrade)
				.HasForeignKey<Solution>(s => s.GradeId);
		}
	}
}
