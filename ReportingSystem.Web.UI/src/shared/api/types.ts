/**
 * This file contains TypeScript type definitions for the data transfer objects (DTOs)
 * used in API communication with the backend. These types provide a contract for
 * client-side data models, ensuring type safety across the application.
 */

// General API Types
export interface PaginatedResponse<T> {
  items: T[];
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface ApiError {
  statusCode: number;
  message: string;
  errors?: Record<string, string[]>;
}

// User and Auth Types
export type UserRole = 'Administrator' | 'Viewer';

export interface User {
  id: string;
  username: string;
  fullName: string;
  email: string;
  role: UserRole;
  isActive: boolean;
  twoFactorEnabled: boolean;
}

// Configuration Entity Types

export interface ConnectorConfiguration {
  id: string;
  name: string;
  connectorType: string;
  lastModified: string; // ISO 8601 Date
}

export interface TransformationScript {
  id: string;
  name: string;
  content: string;
  lastModified: string; // ISO 8601 Date
}

export interface ReportConfiguration {
  id: string;
  name: string;
  description: string | null;
  connectorId: string;
  transformationScriptId: string | null;
  templateId: string | null;
  outputFormat: 'HTML' | 'PDF' | 'JSON' | 'CSV' | 'TXT';
  schedule: string | null;
  isScheduleEnabled: boolean;
  lastModified: string; // ISO 8601 Date
}

// Job Monitoring & Reporting Types
export type JobStatus =
  | 'Queued'
  | 'Running'
  | 'Succeeded'
  | 'Failed'
  | 'Cancelled';

export interface JobExecutionLog {
  id: string;
  reportName: string;
  status: JobStatus;
  startTime: string; // ISO 8601 Date
  endTime: string | null; // ISO 8601 Date
  duration: number | null; // in milliseconds
  triggeredBy: string; // e.g., 'Scheduled' or 'API: admin_user'
  outputFormat: 'HTML' | 'PDF' | 'JSON' | 'CSV' | 'TXT';
}

export interface DetailedJobLogEntry {
  timestamp: string; // ISO 8601 Date
  level: 'INFO' | 'WARN' | 'ERROR' | 'DEBUG' | 'VERBOSE' | 'FATAL';
  stage:
    | 'INITIALIZATION'
    | 'INGESTION'
    | 'TRANSFORMATION'
    | 'GENERATION'
    | 'DELIVERY'
    | 'COMPLETION';
  message: string;
  exception?: string; // Full stack trace if applicable
}

// Audit Log Types
export interface AuditLog {
  id: number;
  timestamp: string; // ISO 8601 Date
  userId: string;
  username: string;
  sourceIpAddress: string;
  actionType: string; // e.g., 'USER_CREATED', 'REPORT_DELETED'
  entityType: string; // e.g., 'User', 'ReportConfiguration'
  entityId: string;
  outcome: 'Success' | 'Failure';
  changeDetails: Record<string, any> | null;
}

// Script Preview Types
export interface ScriptPreviewRequest {
  scriptContent: string;
  sampleData?: string; // JSON string
  connectorId?: string;
}

export interface ScriptPreviewResponse {
  output?: any; // Can be any valid JSON
  error?: {
    message: string;
    stackTrace?: string;
    lineNumber?: number;
  };
  performanceMetrics: {
    executionTimeMs: number;
  };
}