using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Workflow.Api.Models;

namespace Workflow.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkflowsController : ControllerBase
    {
        // Statische Liste zur Speicherung der Workflows
        private static List<Api.Models.Workflow> _workflows = new List<Api.Models.Workflow>();

        [HttpGet]
        public IActionResult GetWorkflows()
        {
            return Ok(_workflows); // Gibt die Liste der Workflows zurück
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkflowById(Guid id)
        {
            var workflow = _workflows.Find(w => w.Id == id);
            if (workflow == null)
            {
                return NotFound(); // Wenn der Workflow nicht gefunden wird
            }
            return Ok(workflow); // Gibt den Workflow zurück
        }

        [HttpPost]
        public IActionResult CreateWorkflow([FromBody] Api.Models.Workflow workflow)
        {
            if (string.IsNullOrEmpty(workflow.Name))
            {
                return BadRequest("Name is mandatory."); 
            }

            workflow.Id = Guid.NewGuid(); // Weist eine neue ID zu
            _workflows.Add(workflow); // Fügt den Workflow zur Liste hinzu

            return Ok(workflow); 
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWorkflow(Guid id, [FromBody] Api.Models.Workflow updatedWorkflow)
        {
            var existingWorkflow = _workflows.Find(w => w.Id == id);
            if (existingWorkflow == null)
            {
                return NotFound(); 
            }

            if (string.IsNullOrEmpty(updatedWorkflow.Name))
            {
                return BadRequest("Name is mandatory."); 
            }

            existingWorkflow.Name = updatedWorkflow.Name; // Aktualisiert den Workflow-Namen
            existingWorkflow.Description = updatedWorkflow.Description; // Aktualisiert die Beschreibung

            return Ok(existingWorkflow); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkflow(Guid id)
        {
            var workflow = _workflows.Find(w => w.Id == id);
            if (workflow == null)
            {
                return NotFound(); 
            }

            _workflows.Remove(workflow); 

            return Ok(); 
        }
    }
}
