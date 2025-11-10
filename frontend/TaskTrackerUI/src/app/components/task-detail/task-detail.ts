import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Task, TaskPriorityLabels, TaskStatusLabels, TaskStatus } from '../../models/task';
import { ApiServiceTs } from '../../services/api.service.ts';

@Component({
  selector: 'app-task-detail',
  imports:[CommonModule, RouterModule],
  templateUrl: './task-detail.html',
  styleUrl: './task-detail.scss',
})
export class TaskDetail implements OnInit {
  task?: Task;
  isLoading = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: ApiServiceTs
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = +params['id'];
      this.loadTask(id);
    });
  }

  loadTask(id: number): void {
    this.isLoading = true;
    this.taskService.getTaskById(id).subscribe({
      next: (task) => {
        this.task = task;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading task:', error);
        this.isLoading = false;
      }
    });
  }

  deleteTask(): void {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(this.task!.id).subscribe({
        next: () => {
          this.router.navigate(['/tasks']);
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

  getTimeAgo(date: Date): string {
    const now = new Date();
    const diffInMs = now.getTime() - new Date(date).getTime();
    const diffInDays = Math.floor(diffInMs / (1000 * 60 * 60 * 24));
    
    if (diffInDays === 0) return 'Today';
    if (diffInDays === 1) return 'Yesterday';
    if (diffInDays < 7) return `${diffInDays} days ago`;
    if (diffInDays < 30) return `${Math.floor(diffInDays / 7)} weeks ago`;
    if (diffInDays < 365) return `${Math.floor(diffInDays / 30)} months ago`;
    return `${Math.floor(diffInDays / 365)} years ago`;
  }
}
