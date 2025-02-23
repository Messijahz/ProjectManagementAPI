import { useState, useEffect } from "react";
import { getProjects } from "../services/projectService";
import { ProjectDTO } from "../types/ProjectDTO";
import "../styles/Home.css";
import ProjectList from "../components/ProjectList";
import Pagination from "../components/Pagination";
import Header from "../components/Header";

const Home = () => {
  const [projects, setProjects] = useState<ProjectDTO[]>([]);
  const [filteredProjects, setFilteredProjects] = useState<ProjectDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [expandedProjectId, setExpandedProjectId] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [projectsPerPage, setProjectsPerPage] = useState(10);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const data = await getProjects();
        setProjects(data);
        setFilteredProjects(data);
      } catch (error) {
        console.error("Failed to fetch projects:", error);
      } finally {
        setLoading(false);
      }
    };
    fetchProjects();
  }, []);

  useEffect(() => {
    if (searchTerm.trim() === "") {
      setFilteredProjects(projects);
    } else {
      setFilteredProjects(
        projects.filter((project) => {
          return (
            project.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
            project.description.toLowerCase().includes(searchTerm.toLowerCase()) ||
            project.status?.statusName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            project.customer.customerName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            project.projectManager.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
            project.projectManager.lastName.toLowerCase().includes(searchTerm.toLowerCase())
          );
        })
      );
    }
  }, [searchTerm, projects]);
  
  

  const refreshProjects = async () => {
    try {
      const data = await getProjects();
      setProjects(data);
      setFilteredProjects(data);
    } catch (error) {
      console.error("Failed to refresh projects:", error);
    }
  };

  const indexOfLastProject = currentPage * projectsPerPage;
  const indexOfFirstProject = indexOfLastProject - projectsPerPage;
  const currentProjects = filteredProjects.slice(indexOfFirstProject, indexOfLastProject);
  const totalPages = Math.ceil(filteredProjects.length / projectsPerPage);

  const toggleExpand = (projectId: number) => {
    setExpandedProjectId(expandedProjectId === projectId ? null : projectId);
  };

  const handleItemsPerPageChange = (items: number) => {
    setProjectsPerPage(items);
    setCurrentPage(1);
  };

  return (
    <div className="container">
      <Header searchTerm={searchTerm} setSearchTerm={setSearchTerm} />
      <div className="content-wrapper">
        {loading ? (
          <p className="loading">Loading projects...</p>
        ) : (
          <>
            <ProjectList
              projects={currentProjects}
              expandedProjectId={expandedProjectId}
              toggleExpand={toggleExpand}
              refreshProjects={refreshProjects}
              searchTerm={searchTerm}
            />
            <Pagination
              currentPage={currentPage}
              totalPages={totalPages}
              projectsPerPage={projectsPerPage}
              onPageChange={setCurrentPage}
              onItemsPerPageChange={handleItemsPerPageChange}
            />
          </>
        )}
      </div>
    </div>
  );
};

export default Home;