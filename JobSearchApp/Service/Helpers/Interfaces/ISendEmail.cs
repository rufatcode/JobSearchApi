using System;
namespace Service.Helpers.Interfaces
{
	public interface ISendEmail
	{
        void Send(string from, string displayName, string to, string messageBody, string subject);
    }
}

