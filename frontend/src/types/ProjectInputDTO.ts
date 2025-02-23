export interface ProjectInputDTO {
    name: string;
    description?: string;
    startDate: string;
    endDate?: string | null;
    statusId: number;
    customerName: string;
    contactPerson: string;
    serviceName: string;
    totalPrice: number;
    projectManagerName: string;
}