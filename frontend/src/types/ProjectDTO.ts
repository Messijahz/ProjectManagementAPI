export interface Status {
    statusId: number;
    statusName: string;
}

export interface Customer {
    customerId: number;
    customerName: string;
    contactPerson: string;
}

export interface Service {
    serviceId: number;
    serviceName: string;
    pricePerUnit: number;
    serviceTypeId: number;
}

export interface ProjectManager {
    projectManagerId: number;
    firstName: string;
    lastName: string;
    email: string;
}

export interface ProjectDTO {
    projectNumber: string;
    projectId: number;
    name: string;
    description: string;
    startDate: string;
    endDate?: string;
    status?: Status;
    customer: Customer;
    service: Service;
    projectManager: ProjectManager;
    totalPrice: number;
}