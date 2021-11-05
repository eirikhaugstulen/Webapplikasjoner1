import React from "react";
import {FormFeedback, FormGroup, Input, Label} from "reactstrap";
import history from "../history";

export const AvgangsVelger = ({ strekninger, strekning }) => {
    console.log(strekning)
    const handleChange = (e) => history.push(`/admin/avgang?strekning=${e.target.value}`)
    
    return (
        <>
            <FormGroup>
                <Label>Strekning</Label>
                <Input
                    type={'select'}
                    onChange={handleChange}
                    value={strekning || ''}
                >
                    <option
                        disabled
                        value={''}
                    >Velg strekning</option>
                    {strekninger?.map(strekning => (
                        <option
                            key={strekning.strekningNummer}
                            value={strekning.strekningNummer}
                        >
                            {strekning.fraSted.stedsnavn} - {strekning.tilSted.stedsnavn}
                        </option>
                    ))}
                </Input>
            </FormGroup>
        </>
    )
}