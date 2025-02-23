import { ProjectDTO } from "../types/ProjectDTO";
import ExpandedProject from "./ExpandedProject";
import { deleteProject } from "../services/projectService";
import { useNavigate } from "react-router-dom";

interface ProjectRowProps {
  project: ProjectDTO;
  expandedProjectId: number | null;
  toggleExpand: (projectId: number) => void;
  refreshProjects: () => void;
}

const ProjectRow = ({ project, expandedProjectId, toggleExpand, refreshProjects }: ProjectRowProps) => {
  const navigate = useNavigate();

  const handleEdit = () => {
    navigate(`/edit/${project.projectId}`);
  };


  const handleDelete = async () => {
    const success = await deleteProject(project.projectId);
    if (success) {
      refreshProjects();
    } else {
      console.error("Failed to delete project.");
    }
  };

  return (
    <>
      <tr>
        <td className="expand-icon">
          <button
            className={expandedProjectId === project.projectId ? "expanded" : ""}
            onClick={() => toggleExpand(project.projectId)}
          >
            ▶
          </button>
        </td>
        <td>{project.name}</td>
        <td>{project.service.serviceName}</td>
        <td>
          <span className={`status-box ${project.status?.statusName.toLowerCase().replace(" ", "-")}`}>
            {project.status?.statusName}
          </span>
        </td>
        <td>{project.customer.customerName}</td>
        <td>{project.projectManager.firstName} {project.projectManager.lastName}</td>
        <td>{project.startDate}</td>
        <td>{project.endDate}</td>
        <td className="action-buttons">
          <div className="button-container">
            <button className="edit-btn" onClick={handleEdit}title="Edit Project">✏️</button>
            <button className="delete-btn" onClick={handleDelete}title="Delete Project">🗑</button>
          </div>
        </td>
      </tr>

      {expandedProjectId === project.projectId && (
        <tr className="expanded-row">
          <td colSpan={9}>
            <ExpandedProject project={project} />
          </td>
        </tr>
      )}
    </>
  );
};

export default ProjectRow;
