using Beauty.Dick.Domain.Impl;
using FluentValidation;
using FluentValidation.Results;
using Jal.Monads;
using System;
using System.Collections.Generic;

namespace Beauty.Dick.Helpers.Domain.Impl
{
    public class Audit : AbstractValueObject
    {
        public string CreateUser { get; private set; }

        public string CreateDevice { get; private set; }

        public DateTime CreateDate { get; private set; }

        public string UpdateUser { get; private set; }

        public string UpdateDevice { get; private set; }

        public DateTime UpdateDate { get; private set; }

        public Int64 TimeSpan { get; private set; }

        public static Audit Create(string user, string device, DateTime dateTime)
        {
            return new Audit()
            {
                CreateUser = user,
                CreateDevice = device,
                CreateDate = dateTime,
                TimeSpan = new TimeSpan().Ticks
            };
        }

        public static Audit Update(Audit audit, string user, string device, DateTime dateTime)
        {
            audit.UpdateUser = user;
            audit.UpdateDevice = device;
            audit.UpdateDate = dateTime;
            audit.TimeSpan = new TimeSpan().Ticks;

            return audit;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TimeSpan;
        }

        protected override Result<ValidationResult> Validate()
        {
            return new AuditValidator().Validate(this);
        }
    }

    class AuditValidator : AbstractValidator<Audit>
    {
        public AuditValidator()
        {
            RuleFor(audit => audit.CreateUser).NotNull();
            RuleFor(audit => audit.CreateDevice).NotNull();
            RuleFor(audit => audit.CreateDate).NotNull();
        }
    }
}
