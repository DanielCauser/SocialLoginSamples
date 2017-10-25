using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleNativeLogin.Services.Contracts
{
	public interface IGoogleManager
	{
		void Login();

		void Logout();
	}
}
