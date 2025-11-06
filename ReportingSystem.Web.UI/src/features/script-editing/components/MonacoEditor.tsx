import React from 'react';
import Editor, { OnChange, OnMount } from '@monaco-editor/react';
import { Box, CircularProgress, useTheme } from '@mui/material';

interface MonacoEditorProps {
  value: string;
  onChange: OnChange;
  language: 'javascript' | 'json';
  readOnly?: boolean;
  height?: string | number;
}

/**
 * A wrapper component for the Monaco Editor.
 * Provides a controlled component interface for editing code with syntax highlighting.
 * Fulfills REQ-UI-DTR-002.
 */
const MonacoEditor: React.FC<MonacoEditorProps> = ({
  value,
  onChange,
  language,
  readOnly = false,
  height = '100%',
}) => {
  const theme = useTheme();

  const handleEditorDidMount: OnMount = (editor, monaco) => {
    // You can add custom editor configurations here
    monaco.languages.typescript.javascriptDefaults.setDiagnosticsOptions({
      noSemanticValidation: false,
      noSyntaxValidation: false,
      // ES6+ features
      lib: ['es2020'],
      target: monaco.languages.typescript.ScriptTarget.ES2020,
    });
    
    // You can also define custom themes or other advanced configurations
    editor.focus();
  };

  return (
    <Box sx={{ height, width: '100%', border: `1px solid ${theme.palette.divider}`, borderRadius: 1, overflow: 'hidden' }}>
      <Editor
        height="100%"
        language={language}
        value={value}
        onChange={onChange}
        onMount={handleEditorDidMount}
        theme={theme.palette.mode === 'dark' ? 'vs-dark' : 'vs'}
        options={{
          readOnly: readOnly,
          minimap: { enabled: false },
          scrollBeyondLastLine: false,
          fontSize: 14,
          wordWrap: 'on',
          automaticLayout: true,
        }}
        loading={
          <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
            <CircularProgress />
          </Box>
        }
      />
    </Box>
  );
};

export default MonacoEditor;