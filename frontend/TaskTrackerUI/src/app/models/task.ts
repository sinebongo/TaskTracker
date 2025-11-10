export interface Task {
  id: number;
  title: string;
  description: string;
  dueDate: Date;
  priority: number; 
  status: number;   
  createdAt: Date;
}

export const TaskPriority = {
  Low: 0,
  Medium: 1,
  High: 2
} as const;

export const TaskStatus = {
  New: 0,
  InProgress: 1,
  Completed: 2
} as const;

export const TaskPriorityLabels = ['Low', 'Medium', 'High'];
export const TaskStatusLabels = ['New', 'InProgress', 'Completed'];