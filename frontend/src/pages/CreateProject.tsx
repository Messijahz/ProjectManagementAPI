import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createProject } from "../services/projectService";
import { ProjectInputDTO } from "../types/ProjectInputDTO";
import Header from "../components/Header";
import ProjectForm from "../components/ProjectForm";
import "../styles/CreateProject.css";

const CreateProject = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [projectCreatedMessage, setProjectCreatedMessage] = useState("");

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
    setProjectCreatedMessage("");

    const dataToSend = {
      ...formData,
      endDate: formData.endDate && formData.endDate !== "" ? formData.endDate : null,
    };

    try {
      const success = await createProject(dataToSend);
      setIsLoading(false);

      if (!success) {
        setErrorMessage("Failed to create project. Please try again.");
      } else {
        setProjectCreatedMessage("Project created successfully!");
        setFormData({
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

        setTimeout(() => {
          navigate("/");
        }, 2000);
      }
    } catch (error) {
      console.error("Error submitting project:", error);
      setErrorMessage("An unexpected error occurred.");
    }
  };

  const handleCancel = () => {
    navigate("/");
  };

  return (
    <div className="container">
      <Header />
      <div className="wrapper">
        <h2>Add New Project</h2>
        <ProjectForm
          formData={formData}
          onChange={handleChange}
          onSubmit={handleSubmit}
          isLoading={isLoading}
          onCancel={handleCancel}
          successMessage={projectCreatedMessage}
          errorMessage={errorMessage}
        />
      </div>
    </div>
  );
};

export default CreateProject;
