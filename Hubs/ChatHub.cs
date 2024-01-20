using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _3abarni_backend.Hubs
{
    public class ChatHub : Hub
    {
        // Maintain a list of connected users
        private static readonly Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();

        public async Task SendMessage(string sender, string receiver, string message)
        {
            // You can perform any additional validation or authorization here

            // Broadcast the message to the sender and receiver
            await Clients.User(receiver).SendAsync("ReceiveMessage", sender, message);
            await Clients.User(sender).SendAsync("ReceiveMessage", sender, message);

            Console.WriteLine($"Message sent: {message} from {sender} to {receiver}");
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var claims = Context.User.Claims.Select(c => $"{c.Type}: {c.Value}");
                Console.WriteLine($"Claims: {string.Join(", ", claims)}");

                var userIdClaim = Context.User.FindFirst("user_id");

                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;

                    ConnectedUsers[userId] = Context.ConnectionId;

                    Console.WriteLine($"User connected with ID: {userId}");

                    // Inform the client about the successful connection
                    await Clients.Caller.SendAsync("ConnectionEstablished", "Connected to the chat.");

                    // Notify others that a new user has joined (optional)
                    await Clients.Others.SendAsync("UserJoined", userId);
                }
                else
                {
                    // Handle the case where the user_id claim is not available
                    Console.WriteLine("User ID claim is null or empty.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
                Console.WriteLine($"Error on connection: {ex.Message}");
            }

            await base.OnConnectedAsync();
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                // Remove disconnected user from the list
                var userIdClaim = Context.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;

                    ConnectedUsers.Remove(userId);

                    // Notify others that a user has left (optional)
                    await Clients.Others.SendAsync("UserLeft", userId);
                }
                else
                {
                    // Handle the case where the user_id claim is not available
                    Console.WriteLine("User ID claim is null or empty.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
                Console.WriteLine($"Error on disconnection: {ex.Message}");
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
