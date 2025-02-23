import { useState, useEffect } from "react";
import { ProjectDTO } from "../types/ProjectDTO";
import ProjectRow from "./ProjectRow";

interface ProjectListProps {
  projects: ProjectDTO[];
  expandedProjectId: number | null;
  toggleExpand: (projectId: number) => void;
  refreshProjects: () => void;
  searchTerm: string;
}

const ProjectList = ({ projects, expandedProjectId, toggleExpand, refreshProjects, searchTerm }: ProjectListProps) => {
  const [filteredProjects, setFilteredProjects] = useState<ProjectDTO[]>(projects);

  useEffect(() => {

    const filtered = projects.filter((project) => {
      const term = searchTerm.toLowerCase();
      return (
        project.name.toLowerCase().includes(term) ||
        project.description.toLowerCase().includes(term) ||
        project.status?.statusName.toLowerCase().includes(term) ||
        project.customer.customerName.toLowerCase().includes(term) ||
        project.projectManager.firstName.toLowerCase().includes(term) ||
        project.projectManager.lastName.toLowerCase().includes(term)
      );
    });

    setFilteredProjects(filtered);
  }, [searchTerm, projects]);

  return (
    <div className="table-container">
      <table className="project-table">
        <thead>
          <tr>
            <th>&nbsp;</th>
            <th>Name</th>
            <th>Service</th>
            <th>Status</th>
            <th>Customer</th>
            <th>Project Manager</th>
            <th>Start Date</th>
            <th>End Date</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredProjects.map((project) => (
            <ProjectRow
              key={project.projectId}
              project={project}
              expandedProjectId={expandedProjectId}
              toggleExpand={toggleExpand}
              refreshProjects={refreshProjects}
            />
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProjectList;
