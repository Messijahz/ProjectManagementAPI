import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getProjectById, updateProject } from "../services/projectService";
import { ProjectInputDTO } from "../types/ProjectInputDTO";
import Header from "../components/Header";
import ProjectForm from "../components/ProjectForm";
import "../styles/CreateProject.css";

const EditProject: React.FC = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [projectUpdatedMessage, setProjectUpdatedMessage] = useState("");
  const [formData, setFormData] = useState<ProjectInputDTO>({
    name: "",
    description: "",
    startDate: "",
    endDate: "",
    statusId: 1,
    customerName: "",
    contactPerson: "",
    serviceName: "",
    projectManagerName: "",
    totalPrice: 0,
  });

  useEffect(() => {
    const fetchProject = async () => {
      if (!id) return;
      setIsLoading(true);
      try {
        const project = await getProjectById(Number(id));
        if (project) {
          setFormData({
            name: project.name,
            description: project.description || "",
            startDate: project.startDate,
            endDate: project.endDate || "",
            statusId: project.status?.statusId || 1,
            customerName: project.customer.customerName,
            contactPerson: project.customer.contactPerson || "",
            serviceName: project.service.serviceName,
            projectManagerName: project.projectManager.firstName,
            totalPrice: project.totalPrice,
          });
        }
      } catch (error) {
        console.error("Error fetching project:", error);
        setErrorMessage("Failed to load project details.");
      } finally {
        setIsLoading(false);
      }
    };

    fetchProject();
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: name === "totalPrice" ? (value === "" ? 0 : Number(value)) : value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsLoading(true);
    setErrorMessage("");
    setProjectUpdatedMessage("");

    const updatedFormData = {
      ...formData,
      endDate: formData.endDate === "" ? null : formData.endDate,
    };

    try {
      const success = await updateProject(Number(id), updatedFormData);
      setIsLoading(false);

      if (!success) {
        setErrorMessage("Failed to update project. Please try again.");
      } else {
        setProjectUpdatedMessage("Project updated successfully!");
        setTimeout(() => {
          navigate("/");
        }, 2000);
      }
    } catch (error) {
      console.error("Error submitting project:", error);
      setErrorMessage("An unexpected error occurred.");
      setIsLoading(false);
    }
  };

  const handleCancel = () => {
    navigate("/");
  };

  return (
    <div className="container">
      <Header />
      <div className="wrapper">
        <h2>Edit Project</h2>
        <ProjectForm
          formData={formData}
          onChange={handleChange}
          onSubmit={handleSubmit}
          isLoading={isLoading}
          onCancel={handleCancel}
          successMessage={projectUpdatedMessage}
          errorMessage={errorMessage}
        />
      </div>
    </div>
  );
};

export default EditProject;