using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using InfinityStudio.Models;
using InfinityStudio.Models.Repositories;

namespace InfinityStudio.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount { get => _currentUserAccount; 
            set 
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            } 
        }

        public MainViewModel() 
        {
            CurrentUserAccount = new UserAccountModel();
            userRepository = new UserRepository();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null) 
            {
                CurrentUserAccount.Username = user.UserName;
                CurrentUserAccount.DisplayName = $"Welcome {user.FirstName} {user.LastName} ;)";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                MessageBox.Show("Invalid user, not logged in");
            }
        }
    }
}
