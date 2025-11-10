import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Task, TaskPriorityLabels, TaskStatusLabels, TaskStatus } from '../../models/task';
import { ApiServiceTs } from '../../services/api.service.ts';

@Component({
  selector: 'app-task-list',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss',
})
export class TaskList implements OnInit {
  tasks: Task[] = [];
  filteredTasks: Task[] = [];
  searchTerm: string = '';
  sortBy: string = '';
  isLoading: boolean = false;

  constructor(private taskService: ApiServiceTs) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading = true;
    this.taskService.getAllTasks(this.searchTerm, this.sortBy).subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        this.filteredTasks = tasks;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading tasks:', error);
        this.isLoading = false;
      }
    });
  }

  onSearch(): void {
    this.loadTasks();
  }

  onSortChange(): void {
    this.loadTasks();
  }

  deleteTask(id: number): void {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(id).subscribe({
        next: () => {
          this.tasks = this.tasks.filter(task => task.id !== id);
          this.filteredTasks = this.filteredTasks.filter(task => task.id !== id);
        },
        error: (error) => console.error('Error deleting task:', error)
      });
    }
  }

  getPriorityClass(priority: number): string {
    switch (priority) {
      case 2: return 'priority-high';   // High
      case 1: return 'priority-medium'; // Medium
      case 0: return 'priority-low';    // Low
      default: return '';
    }
  }

  getPriorityLabel(priority: number): string {
    return TaskPriorityLabels[priority] || 'Unknown';
  }

  getStatusLabel(status: number): string {
    return TaskStatusLabels[status] || 'Unknown';
  }

  isCompleted(status: number): boolean {
    return status === TaskStatus.Completed;
  }

  trackByTaskId(index: number, task: Task): number {
    return task.id;
  }

  getCompletedCount(): number {
    return this.filteredTasks.filter(task => this.isCompleted(task.status)).length;
  }

  getPendingCount(): number {
    return this.filteredTasks.filter(task => !this.isCompleted(task.status)).length;
  }

  getStatusClass(status: number): string {
    switch (status) {
      case 0: return 'status-new';        // New
      case 1: return 'status-progress';   // InProgress
      case 2: return 'status-completed';  // Completed
      default: return 'status-unknown';
    }
  }

  isOverdue(dueDate: Date): boolean {
    if (!dueDate) return false;
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    const due = new Date(dueDate);
    due.setHours(0, 0, 0, 0);
    return due < today;
  }
}
