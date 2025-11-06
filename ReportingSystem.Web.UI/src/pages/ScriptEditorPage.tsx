import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
    Box,
    Button,
    CircularProgress,
    Container,
    Typography,
    Alert,
    Stack,
    TextField,
} from '@mui/material';
import { Panel, PanelGroup, PanelResizeHandle } from 'react-resizable-panels';
import { PageHeader } from '../widgets/PageHeader';
import { MonacoEditor } from '../features/script-editing/components/MonacoEditor';
import { useScriptsApi } from '../features/script-editing/api/useScriptsApi';
import { useDebounce } from '../shared/lib/hooks/useDebounce';
import { ConfirmationModal } from '../shared/ui/ConfirmationModal';

const ScriptEditorPage: React.FC = () => {
    const { scriptId } = useParams<{ scriptId: string }>();
    const navigate = useNavigate();

    const isNewScript = !scriptId;

    const [scriptName, setScriptName] = useState('');
    const [scriptContent, setScriptContent] = useState('function transform(data) {\n  // Your transformation logic here\n  return data;\n}');
    const [sampleData, setSampleData] = useState('[\n  {\n    "id": 1,\n    "name": "Sample Item"\n  }\n]');
    const [output, setOutput] = useState('');
    const [isDirty, setIsDirty] = useState(false);
    const [showCancelConfirm, setShowCancelConfirm] = useState(false);

    const debouncedScriptContent = useDebounce(scriptContent, 500);

    const { getScriptById, createScript, updateScript, previewScript } = useScriptsApi();

    const { data: existingScript, isLoading: isLoadingScript } = getScriptById(scriptId!, {
        enabled: !isNewScript,
    });

    useEffect(() => {
        if (existingScript) {
            setScriptName(existingScript.name);
            setScriptContent(existingScript.content);
            setIsDirty(false); // Reset dirty state after loading
        }
    }, [existingScript]);

    const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setScriptName(event.target.value);
        setIsDirty(true);
    };

    const handleScriptChange = (value: string | undefined) => {
        setScriptContent(value || '');
        setIsDirty(true);
    };

    const handleSampleDataChange = (value: string | undefined) => {
        setSampleData(value || '');
    };
    
    const handlePreview = () => {
        previewScript.mutate(
            { scriptContent, sampleData },
            {
                onSuccess: (data) => {
                    setOutput(JSON.stringify(data, null, 2));
                },
                onError: (error) => {
                    const errorMessage = error.response?.data?.error || { message: 'An unknown error occurred' };
                    setOutput(JSON.stringify(errorMessage, null, 2));
                },
            }
        );
    };

    const handleSave = () => {
        const scriptData = { name: scriptName, content: scriptContent };
        const mutationOptions = {
            onSuccess: () => {
                setIsDirty(false);
                navigate('/transformations');
            },
            onError: (error: any) => {
                // Here you would set an error state to show in a toast/alert
                console.error("Save failed:", error.response?.data?.error);
                setOutput(JSON.stringify(error.response?.data || { message: 'Failed to save script.' }, null, 2));
            },
        };

        if (isNewScript) {
            createScript.mutate(scriptData, mutationOptions);
        } else {
            updateScript.mutate({ id: scriptId, ...scriptData }, mutationOptions);
        }
    };

    const handleCancel = () => {
        if (isDirty) {
            setShowCancelConfirm(true);
        } else {
            navigate('/transformations');
        }
    };

    const isSaving = createScript.isPending || updateScript.isPending;

    if (isLoadingScript) {
        return <CircularProgress />;
    }

    return (
        <Container maxWidth={false}>
            <PageHeader
                title={isNewScript ? 'Create Transformation Script' : `Edit: ${existingScript?.name || ''}`}
                breadcrumbs={[{ label: 'Transformations', path: '/transformations' }, { label: isNewScript ? 'Create' : 'Edit' }]}
            />
            <Stack spacing={2} sx={{ mb: 2 }}>
                <TextField
                    label="Script Name"
                    value={scriptName}
                    onChange={handleNameChange}
                    variant="outlined"
                    fullWidth
                    required
                    disabled={isSaving}
                />
            </Stack>
            <PanelGroup direction="horizontal" style={{ height: '70vh', border: '1px solid #ccc' }}>
                <Panel defaultSize={50} minSize={20}>
                    <Typography variant="h6" sx={{ p: 1, backgroundColor: 'grey.200' }}>
                        Script Editor
                    </Typography>
                    <MonacoEditor language="javascript" value={scriptContent} onChange={handleScriptChange} />
                </Panel>
                <PanelResizeHandle style={{ width: '8px', background: '#eee', cursor: 'col-resize' }} />
                <Panel minSize={20}>
                    <PanelGroup direction="vertical">
                        <Panel defaultSize={50} minSize={20}>
                            <Typography variant="h6" sx={{ p: 1, backgroundColor: 'grey.200' }}>
                                Sample JSON Input
                            </Typography>
                            <MonacoEditor language="json" value={sampleData} onChange={handleSampleDataChange} />
                        </Panel>
                        <PanelResizeHandle style={{ height: '8px', background: '#eee', cursor: 'row-resize' }} />
                        <Panel minSize={20}>
                             <Typography variant="h6" sx={{ p: 1, backgroundColor: 'grey.200' }}>
                                Preview Output / Errors
                            </Typography>
                            <MonacoEditor language="json" value={output} options={{ readOnly: true }} />
                        </Panel>
                    </PanelGroup>
                </Panel>
            </PanelGroup>

            <Stack direction="row" spacing={2} sx={{ mt: 2 }}>
                <Button variant="contained" onClick={handleSave} disabled={isSaving || !scriptName || !scriptContent}>
                    {isSaving ? <CircularProgress size={24} /> : 'Save'}
                </Button>
                <Button variant="outlined" onClick={handleCancel}>
                    Cancel
                </Button>
                <Button variant="contained" color="secondary" onClick={handlePreview} disabled={previewScript.isPending}>
                    {previewScript.isPending ? <CircularProgress size={24} /> : 'Preview'}
                </Button>
            </Stack>

            {previewScript.isError && (
                 <Alert severity="error" sx={{ mt: 2 }}>
                    Preview failed: {previewScript.error.response?.data?.error?.message || previewScript.error.message}
                 </Alert>
            )}

            <ConfirmationModal
                open={showCancelConfirm}
                title="Discard Unsaved Changes?"
                message="You have unsaved changes. Are you sure you want to discard them and return to the list?"
                onConfirm={() => {
                    setShowCancelConfirm(false);
                    navigate('/transformations');
                }}
                onCancel={() => setShowCancelConfirm(false)}
            />
        </Container>
    );
};

export default ScriptEditorPage;