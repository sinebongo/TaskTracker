import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ApiServiceTs } from '../../services/api.service.ts';
import { Task, TaskPriority, TaskStatus, TaskPriorityLabels, TaskStatusLabels } from '../../models/task';

@Component({
  selector: 'app-task-form',
  imports:  [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './task-form.html',
  styleUrl: './task-form.scss',
})
export class TaskForm implements OnInit {
  taskForm: FormGroup;
  isEditMode = false;
  taskId?: number;
  isLoading = false;

  priorities = TaskPriorityLabels.map((label, index) => ({ label, value: index }));
  statuses = TaskStatusLabels.map((label, index) => ({ label, value: index }));

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private taskService: ApiServiceTs
  ) {
    this.taskForm = this.createForm();
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.isEditMode = true;
        this.taskId = +params['id'];
        this.loadTask(this.taskId);
      }
    });
  }

  createForm(): FormGroup {
    return this.fb.group({
      title: ['', Validators.required],
      description: [''],
      dueDate: [''],
      priority: [TaskPriority.Medium, Validators.required],
      status: [TaskStatus.New, Validators.required]
    });
  }

  loadTask(id: number): void {
    this.isLoading = true;
    this.taskService.getTaskById(id).subscribe({
      next: (task) => {
        this.taskForm.patchValue({
          title: task.title,
          description: task.description,
          dueDate: task.dueDate ? new Date(task.dueDate).toISOString().split('T')[0] : '',
          priority: task.priority,
          status: task.status
        });
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading task:', error);
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      this.isLoading = true;
      const formValue = this.taskForm.value;
      const taskData: Task = {
        id: this.taskId || 0,
        title: formValue.title,
        description: formValue.description,
        dueDate: formValue.dueDate ? new Date(formValue.dueDate) : new Date(),
        priority: formValue.priority,
        status: formValue.status,
        createdAt: new Date()
      };

      if (this.isEditMode) {
        this.taskService.updateTask(this.taskId!, taskData).subscribe({
          next: () => {
            this.router.navigate(['/tasks']);
          },
          error: (error) => {
            console.error('Error saving task:', error);
            this.isLoading = false;
          }
        });
      } else {
        this.taskService.createTask(taskData).subscribe({
          next: () => {
            this.router.navigate(['/tasks']);
          },
          error: (error) => {
            console.error('Error saving task:', error);
            this.isLoading = false;
          }
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/tasks']);
  }
}