import axios from "axios";
import { ProjectDTO} from "../types/ProjectDTO";
import { ProjectInputDTO} from "../types/ProjectInputDTO";

const API_URL = "http://localhost:5177/api/Project";

export const getProjects = async (): Promise<ProjectDTO[]> => {
  try {
    const response = await axios.get(API_URL);
    return response.data;
  } catch (error) {
    console.error("Error fetching projects:", error);
    return [];
  }
};

export const getProjectById = async (id: number): Promise<ProjectDTO | null> => {
  try {
    const response = await axios.get<ProjectDTO>(`${API_URL}/${id}`);
    return response.data;
  } catch (error) {
    console.error(`Error fetching project with ID ${id}:`, error);
    return null;
  }
};

export const createProject = async (projectData: ProjectInputDTO): Promise<boolean> => {
  try {
    const response = await axios.post(API_URL, projectData);
    return response.status === 201;
  } catch (error) {
    console.error("Error creating project:", error);
    return false;
  }
};

export const updateProject = async (id: number, projectData: ProjectInputDTO): Promise<boolean> => {
  try {
    const response = await axios.put(`${API_URL}/${id}`, projectData);
    return response.status === 200;
  } catch (error) {
    console.error("Error updating project:", error);
    return false;
  }
};

export const deleteProject = async (id: number): Promise<boolean> => {
  try {
    const response = await axios.delete(`${API_URL}/${id}`);
    return response.status === 204;
  } catch (error) {
    console.error(`Error deleting project with ID ${id}:`, error);
    return false;
  }
};