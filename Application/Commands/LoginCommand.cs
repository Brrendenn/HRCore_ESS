using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class LoginCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public class Handler : IRequestHandler<LoginCommand, string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(IApplicationDbContext dbContext, IJwtProvider jwtProvider,  IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.EmployeeEmail == request.Email, cancellationToken);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            bool isValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
            if (!isValid) throw new Exception("Invalid Credentials");

            string token = _jwtProvider.Generate(user);
            return token;
        }
    }
}