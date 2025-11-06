/**
 * A collection of pure utility functions for formatting data for display in the UI.
 */

/**
 * Formats an ISO 8601 date string or a Date object into a human-readable format.
 * Uses the Intl.DateTimeFormat API for locale-aware formatting.
 *
 * @param {string | Date | null | undefined} date The date to format.
 * @param {Intl.DateTimeFormatOptions} options Optional formatting options.
 * @returns {string} The formatted date string, or 'N/A' if the input is invalid.
 */
export const formatDateTime = (
  date: string | Date | null | undefined,
  options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: false,
  }
): string => {
  if (!date) {
    return 'N/A';
  }

  try {
    const dateObj = typeof date === 'string' ? new Date(date) : date;
    // Check if the date is valid
    if (isNaN(dateObj.getTime())) {
      return 'N/A';
    }
    return new Intl.DateTimeFormat(navigator.language, options).format(dateObj);
  } catch (error) {
    console.error('Failed to format date:', date, error);
    return 'N/A';
  }
};

/**
 * Formats a duration in milliseconds into a human-readable string (e.g., '1m 15s', '32s 123ms').
 *
 * @param {number | null | undefined} milliseconds The duration in milliseconds.
 * @returns {string} The formatted duration string, or 'N/A' if the input is invalid.
 */
export const formatDuration = (
  milliseconds: number | null | undefined
): string => {
  if (milliseconds === null || milliseconds === undefined || milliseconds < 0) {
    return 'N/A';
  }

  if (milliseconds === 0) {
    return '0ms';
  }

  const hours = Math.floor(milliseconds / 3600000);
  const minutes = Math.floor((milliseconds % 3600000) / 60000);
  const seconds = Math.floor((milliseconds % 60000) / 1000);
  const ms = milliseconds % 1000;

  const parts: string[] = [];
  if (hours > 0) {
    parts.push(`${hours}h`);
  }
  if (minutes > 0) {
    parts.push(`${minutes}m`);
  }
  if (seconds > 0) {
    parts.push(`${seconds}s`);
  }
  if (ms > 0 && parts.length < 2) {
    // Only show milliseconds if the duration is less than a minute for brevity
    parts.push(`${ms}ms`);
  }
  
  return parts.join(' ') || '0ms';
};

/**
 * Truncates a string to a specified length and appends an ellipsis.
 *
 * @param {string} text The text to truncate.
 * @param {number} maxLength The maximum length of the string.
 * @returns {string} The truncated string.
 */
export const truncate = (text: string, maxLength: number = 50): string => {
    if(!text) return '';
    if (text.length <= maxLength) {
        return text;
    }
    return `${text.substring(0, maxLength)}...`;
};