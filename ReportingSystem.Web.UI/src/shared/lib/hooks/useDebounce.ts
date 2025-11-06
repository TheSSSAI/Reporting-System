import { useState, useEffect } from 'react';

/**
 * A custom React hook that debounces a value.
 * It delays updating the output value until the input value has stopped changing for a specified time.
 * This is useful for performance-intensive operations like API calls on user input (e.g., search).
 *
 * @template T The type of the value to be debounced.
 * @param {T} value The value to debounce.
 * @param {number} delay The debounce delay in milliseconds. Defaults to 500ms.
 * @returns {T} The debounced value.
 */
function useDebounce<T>(value: T, delay: number = 500): T {
  // State to store the debounced value
  const [debouncedValue, setDebouncedValue] = useState<T>(value);

  useEffect(
    () => {
      // Set up a timer to update the debounced value after the specified delay
      const handler = setTimeout(() => {
        setDebouncedValue(value);
      }, delay);

      // Clean up the timer if the value changes or the component unmounts.
      // This is the core of the debounce logic: it prevents the previous
      // timer from firing if a new value comes in before the delay is over.
      return () => {
        clearTimeout(handler);
      };
    },
    // Only re-call effect if value or delay changes
    [value, delay]
  );

  return debouncedValue;
}

export default useDebounce;