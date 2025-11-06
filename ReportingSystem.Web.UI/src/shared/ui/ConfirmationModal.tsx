import React from 'react';
import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  CircularProgress,
} from '@mui/material';

export interface ConfirmationModalProps {
  /**
   * Controls if the modal is open.
   */
  open: boolean;
  /**
   * Title of the confirmation dialog.
   */
  title: string;
  /**
   * The main content/question of the dialog.
   */
  content: React.ReactNode;
  /**
   * Callback fired when the user cancels or closes the dialog.
   */
  onClose: () => void;
  /**
   * Callback fired when the user confirms the action.
   */
  onConfirm: () => void;
  /**
   * Text for the confirmation button. Defaults to 'Confirm'.
   */
  confirmText?: string;
  /**
   * Text for the cancellation button. Defaults to 'Cancel'.
   */
  cancelText?: string;
  /**
   * The color/variant for the confirmation button. Useful for destructive actions. Defaults to 'primary'.
   */
  confirmButtonColor?: 'primary' | 'secondary' | 'error' | 'info' | 'success' | 'warning';
  /**
   * If true, shows a loading spinner on the confirm button and disables both buttons.
   */
  isConfirming?: boolean;
}

/**
 * A reusable modal dialog for confirming user actions, such as deletions.
 * It provides a consistent UX for critical, potentially destructive operations.
 *
 * @param {ConfirmationModalProps} props - The component props.
 * @returns {React.ReactElement} The rendered confirmation modal.
 */
const ConfirmationModal: React.FC<ConfirmationModalProps> = ({
  open,
  title,
  content,
  onClose,
  onConfirm,
  confirmText = 'Confirm',
  cancelText = 'Cancel',
  confirmButtonColor = 'primary',
  isConfirming = false,
}) => {
  return (
    <Dialog
      open={open}
      onClose={onClose}
      aria-labelledby="confirmation-dialog-title"
      aria-describedby="confirmation-dialog-description"
    >
      <DialogTitle id="confirmation-dialog-title">{title}</DialogTitle>
      <DialogContent>
        <DialogContentText id="confirmation-dialog-description">
          {content}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} disabled={isConfirming} color="inherit">
          {cancelText}
        </Button>
        <Button
          onClick={onConfirm}
          color={confirmButtonColor}
          variant="contained"
          disabled={isConfirming}
          startIcon={isConfirming ? <CircularProgress size={20} color="inherit" /> : null}
          autoFocus
        >
          {isConfirming ? 'Processing...' : confirmText}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmationModal;