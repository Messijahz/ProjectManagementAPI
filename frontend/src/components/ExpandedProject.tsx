import { ProjectDTO } from "../types/ProjectDTO";

interface ExpandedProjectProps {
  project: ProjectDTO;
}

const ExpandedProject = ({ project }: ExpandedProjectProps) => {
  return (
    <div className="expanded-content">

      <div className="expanded-item">
        <span className="icon-id"><i className="fas fa-hashtag"></i></span>
        <strong>Project Number:</strong>
      </div>
      <div className="expanded-subitem">{project.projectNumber}</div>

      <div className="expanded-item">
        <span className="icon-status"><i className="fas fa-bolt"></i></span>
        <strong>Status:</strong>
      </div>
      <div className="expanded-subitem">
        <div className={`status-badge ${project.status?.statusName?.toLowerCase().replace(/\s+/g, '-')}`}>
          {project.status ? project.status.statusName : "Unknown"}
        </div>
      </div>

      <div className="expanded-item">
        <span className="icon-description"><i className="fas fa-file-alt"></i></span>
        <strong>Description:</strong>
      </div>
      <div className="expanded-subitem">{project.description}</div>

      <div className="expanded-item">
        <span className="icon-service"><i className="fas fa-cogs"></i></span>
        <strong>Service:</strong>
      </div>
      <div className="expanded-subitem">{project.service.serviceName}</div>

      <div className="expanded-item">
        <span className="icon-calendar"><i className="fas fa-calendar-day"></i></span>
        <strong>Start-Date:</strong>
      </div>
      <div className="expanded-subitem">{project.startDate}</div>

      <div className="expanded-item">
        <span className="icon-calendar"><i className="fas fa-calendar-day"></i></span>
        <strong>End-Date:</strong>
      </div>
      <div className="expanded-subitem">{project.endDate}</div>

      <div className="expanded-item">
        <span className="icon-manager"><i className="fas fa-user-tie"></i></span>
        <strong>Project Manager:</strong>
      </div>
      <div className="expanded-subitem">{project.projectManager.firstName} {project.projectManager.lastName}</div>

      <div className="expanded-item">
        <span className="icon-customer"><i className="fas fa-building"></i></span>
        <strong>Customer:</strong>
      </div>
      <div className="expanded-subitem">{project.customer.customerName}</div>

      <div className="expanded-item">
        <span className="icon-person"><i className="fas fa-user"></i></span>
        <strong>Contact Person:</strong>
      </div>
      <div className="expanded-subitem">{project.customer.contactPerson}</div>
    </div>
  );
};

export default ExpandedProject;