using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using React_project.Server.Interfaces;
using React_project.Server.Models;
using React_project.Server.Models.ViewModels;

namespace React_project.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientService _clientService;
        public ClientController(IClientRepository clientRepository, ClientService clientService)
        {
            _clientRepository = clientRepository;
            _clientService = clientService;
        }

        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientRepository.GetAllClients();
                return Ok(clients);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetClientById")]
        public async Task<IActionResult> GetClientById([FromQuery] Guid id)
        {
            try
            {
                var client = await _clientRepository.GetClientById(id);
                return Ok(client);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("GetClientByName")]
        public async Task<IActionResult> GetClientByName([FromQuery] string name)
        {
            try
            {
                var client = await _clientRepository.GetClientByName(name);
                return Ok(client);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetClientByEmail")]
        public async Task<IActionResult> GetClientByEmail([FromQuery] string email)
        {
            try
            {
                var client = await _clientRepository.GetClientByName(email);
                return Ok(client);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet]
        [Route("GetClientByDocument")]
        public async Task<IActionResult> GetClientByDocument([FromQuery] string document)
        {
            try
            {
                var client = await _clientRepository.GetClientByName(document);
                return Ok(client);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("InactiveClient")]
        public async Task<IActionResult> InactiveClient([FromQuery] Guid id)
        {
            try
            {
                var client = await _clientRepository.GetClientById(id);
                if (client is not null)
                {
                    client.operation = "inactive";

                    var clientData = JsonConvert.SerializeObject(client);
                    _clientService.SendMessage(clientData);
                }
                return Ok();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteClientById")]
        public async Task<IActionResult> DeleteClientById([FromQuery] Guid id)
        {
            try
            {
                var client = await _clientRepository.GetClientById(id);
                if (client is not null)
                {
                    client.operation = "delete";

                    var clientData = JsonConvert.SerializeObject(client);
                    _clientService.SendMessage(clientData);
                }

                return Ok();
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDTO clientDTO)
        {
            try
            {
                var client = new Client
                {
                    Name = clientDTO.Name,
                    Email = clientDTO.Email,
                    document = clientDTO.document,
                    phone = clientDTO.phone,
                    operation = "create"
                };

                var clientData = JsonConvert.SerializeObject(client);
                _clientService.SendMessage(clientData);

                return Ok(client);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        [Route("UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientDTO updateClientDTO)
        {
            try
            {
                var client = new Client
                {
                    Name = updateClientDTO.Name,
                    Email = updateClientDTO.Email,
                    document = updateClientDTO.document,
                    phone = updateClientDTO.phone,
                    active = updateClientDTO.active,
                    operation = "update"
                };

                var clientData = JsonConvert.SerializeObject(client);
                _clientService.SendMessage(clientData);

                return Ok(client);
            }
            catch (Exception e)
            {
                SentrySdk.CaptureException(e);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
