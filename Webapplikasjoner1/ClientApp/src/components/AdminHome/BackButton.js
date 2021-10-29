import React from "react";
import history from "../../history";
import {ArrowLeftIcon} from "@primer/octicons-react";
import {Button} from "reactstrap";

export const BackButton = () => (
    <Button
        color={'light'}
        size={'sm'}
        onClick={() => history.push('/admin')}
    >
        <div className={'d-flex justify-content-between align-items-center'} style={{gap: '5px'}}>
            <ArrowLeftIcon size={20} />
            Tilbake
        </div>
    </Button>
)