import React from "react";
import { ProjectInputDTO } from "../types/ProjectInputDTO";

interface ProjectFormProps {
  formData: ProjectInputDTO;
  onChange: (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => void;
  onSubmit: (e: React.FormEvent<HTMLFormElement>) => void;
  isLoading: boolean;
  onCancel: () => void;
  successMessage?: string;
  errorMessage?: string;
}

const ProjectForm: React.FC<ProjectFormProps> = ({
  formData,
  onChange,
  onSubmit,
  isLoading,
  onCancel,
  successMessage,
  errorMessage,
}) => {
  return (
    <form onSubmit={onSubmit}>
      {successMessage && <div className="success-message">{successMessage}</div>}
      {errorMessage && <div className="error-message">{errorMessage}</div>}

      <div className="input-group">
        <input
          type="text"
          name="name"
          value={formData.name}
          onChange={onChange}
          required
          placeholder="Project Name *"
        />
      </div>

      <div className="input-group">
        <textarea
          name="description"
          value={formData.description}
          onChange={onChange}
          required
          placeholder="Description *"
        />
      </div>

      <div className="row">
        <div className="input-group">
          <input
            type="date"
            name="startDate"
            value={formData.startDate}
            onChange={onChange}
            required
            placeholder="Start Date *"
          />
        </div>
        <div className="input-group">
          <input
            type="date"
            name="endDate"
            value={formData.endDate || ""}
            onChange={onChange}
            placeholder="End Date"
          />
        </div>
      </div>

      <div className="row">
        <div className="input-group">
          <select name="serviceName" value={formData.serviceName} onChange={onChange} required>
            <option value="" disabled>Select a service *</option>
            <option value="IT Consulting">IT Consulting</option>
            <option value="Software Development">Software Development</option>
            <option value="Project Management">Project Management</option>
            <option value="Business Analysis">Business Analysis</option>
          </select>
        </div>

        <div className="input-group">
          <select name="statusId" value={formData.statusId.toString()} onChange={onChange} required>
            <option value="1">Not Started</option>
            <option value="2">In Progress</option>
            <option value="3">On Hold</option>
            <option value="4">Completed</option>
          </select>
        </div>
      </div>

      <div className="input-group">
        <input
          type="text"
          name="customerName"
          value={formData.customerName}
          onChange={onChange}
          required
          placeholder="Customer *"
        />
      </div>

      <div className="input-group">
        <input
          type="text"
          name="projectManagerName"
          value={formData.projectManagerName}
          onChange={onChange}
          required
          placeholder="Project Manager *"
        />
      </div>

      <div className="input-group">
        <input
          type="text"
          name="contactPerson"
          value={formData.contactPerson}
          onChange={onChange}
          required
          placeholder="Contact Person *"
        />
      </div>

      <div className="input-group">
        <input
          type="number"
          name="totalPrice"
          value={formData.totalPrice === 0 ? "" : formData.totalPrice}
          onChange={onChange}
          required
          placeholder="Total Price *"
          min="0"
        />
      </div>

      <div className="form-actions">
        <button type="button" className="cancel-btn" onClick={onCancel}>
          Cancel
        </button>
        <button type="submit" className="save-btn" disabled={isLoading}>
          {isLoading ? "Saving..." : "Save"}
        </button>
      </div>
    </form>
  );
};

export default ProjectForm;