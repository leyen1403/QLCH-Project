using FluentValidation;
using QLCH.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.Application.Validators
{
    public class ChucVuDtoValidator : AbstractValidator<ChucVuDto>
    {
        public ChucVuDtoValidator()
        {
            RuleFor(x => x.TenChucVu)
                .NotEmpty().WithMessage("Tên chức vụ không được để trống.")
                .MaximumLength(100).WithMessage("Tên chức vụ không được vượt quá 100 ký tự.");

            RuleFor(x => x.HeSoLuong)
                .GreaterThanOrEqualTo(1.0m).WithMessage("Hệ số lương phải >= 1.0");

            RuleFor(x => x.MoTa)
                .MaximumLength(255).WithMessage("Mô tả không được vượt quá 255 ký tự.");
        }
    }
}
