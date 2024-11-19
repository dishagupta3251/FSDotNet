using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BusTicketingApp.EmailInterface;
using BusTicketingApp.EmailModels;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Services
{
    public class UserService : IUserServices
    {
        private readonly IRepository<User, string> _userRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<BusOperator,int> _busOperatorRepository;
        private readonly IRepository<Customer,int> _customerRepository;
        private readonly IEmailSender _emailSender;
        private readonly ITokenService _tokenService;
      
        public UserService(IRepository<User, string> repository, ITokenService tokenService, IMapper mapper, IRepository<Customer,int> customerRepository, IRepository<BusOperator,int> busOperatorRepository, IEmailSender emailSender)
        {
            _userRepository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _busOperatorRepository = busOperatorRepository;
            _emailSender = emailSender;
        }
        public async Task<OperationStatusDTO> Delete(string key)
        {
            var _user = await _userRepository.Delete(key);
            string status = "Delete Failed";
            if (_user != null)
            {
                status = "Delete Successful";
            }
            var result = new OperationStatusDTO()
            {
                Username = _user.Username,
                Status = status
            };
            return result;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                if (users.Count() == 0) throw new Exception("Cannot fetch all users");

                return users;
            }
            catch
            {
                throw new Exception("Error");
            }

        }

        public async Task<UserProfileDTO> GetById(string username)
        {
            var _user = await _userRepository.Get(username);
            var user = _mapper.Map<UserProfileDTO>(_user);
            return user;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {

            var user = await _userRepository.Get(loginRequestDTO.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(user.PasswordHash);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequestDTO.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.Password[i])
                {
                    throw new Exception("Invalid  password");
                }
            }
            return new LoginResponseDTO()
            {
                Username = user.Username,
                Token = await _tokenService.GenerateToken(new UserTokenDTO()
                {
                    Username = user.Username,
                    Role = user.Role.ToString()
                }),
                UserId=user.UserId

            };
        }
      

        

        public async Task<string> Register(UserRegisterDTO user)
        {
            try
            {
                HMACSHA256 hmac = new HMACSHA256();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

                User _user = new User
                {
                    FirstName = user.FirstName,
                    Username = user.FirstName + user.ContactNumber.Substring(7),
                    Password = passwordHash,
                    PasswordHash = hmac.Key,
                    Role = user.Role
                };

                var addedUser = await _userRepository.Add(_user);
                if (addedUser != null)
                {
                    string body =$@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            border-radius: 8px;
        }}
        .header {{
            text-align: center;
            padding-bottom: 20px;
            border-bottom: 1px solid #ddd;
        }}
        .header h1 {{
            color: #4CAF50;
        }}
        .content {{
            margin-top: 20px;
        }}
        .footer {{
            margin-top: 30px;
            text-align: center;
            font-size: 0.9em;
            color: #666;
        }}
        .button {{
            display: inline-block;
            margin-top: 20px;
            padding: 10px 20px;
            background-color: #4CAF50;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            font-weight: bold;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Welcome!</h1>
        </div>
        <div class='content'>
            <p>Dear {addedUser.FirstName},</p>
            <p>Thank you for joining. We’re excited to have you on board!</p>
            <p><strong>Your Registration Details:</strong></p>
            <ul>
                <li><strong>Username:</strong> {addedUser.Username}</li>
                <li><strong>Email:</strong> {user.Email}</li>
            </ul>
            <p>You can now log in and explore our services tailored just for you.</p>
            <a href='https://localhost:7176/api/User/Login' class='button'>Login to Your Account</a>
           
        </div>
        <div class='footer'>
            <p>Thank you,</p>
            <p><strong>The [YourAppName] Team</strong></p>
        </div>
    </div>
</body>
</html>";

                    var rng = new Random();
                    var message = new Message(new string[] {
                        user.Email
                         },
                            "Congratulations! Your Registration is Complete",
                            body);
                    _emailSender.SendEmail(message);
                   


                    if (addedUser.Role == Roles.BusOperator)
                    {
                        var newOperator = new BusOperator
                        {
                            OperatorContact = user.ContactNumber,
                            OperatorName =user.FirstName + " " + user.LastName,
                            Email = user.Email,
                            UserId=addedUser.UserId
                            
                        };

                        var busOperator=await _busOperatorRepository.Add(newOperator);
                        if (busOperator == null)
                        {
                            throw new Exception("BusOperator is cannot be added");
                        }
                    }
                    else if(addedUser.Role ==Roles.Customer)
                    {
                        var newCustomer = new Customer
                        {
                            CustomerName = user.FirstName + " " + user.LastName,
                            Contact=user.ContactNumber,
                            Email=user.Email,
                            UserId=addedUser.UserId,
                          

                        };
                        var customer=await _customerRepository.Add(newCustomer);
                        if (customer == null) throw new Exception("Customer cannot be added");
                    }
                }
              
                return _user.Username;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        public async Task<OperationStatusDTO> UpdatePassword(string username,string password)
        {
            var user = await _userRepository.Get(username);
            using var hmac = new HMACSHA256();
            user.PasswordHash= hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var result = await _userRepository.Update(user,username);

            return new OperationStatusDTO
            {
                Username = result.Username,
                Status = "Update Successful"
            };
        }


    }


}
