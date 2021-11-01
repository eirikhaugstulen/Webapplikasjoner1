import React from "react";
import { Formik } from "formik";
import {Button, FormFeedback, FormGroup, Input} from "reactstrap";
import * as Yup from 'yup';

export const LeggTilLokasjon = () => (
    <Formik 
        initialValues={{
            lokasjon: ''
        }}
        onSubmit={async values => {
            alert(JSON.stringify(values, null, 2))
        }}
        validationSchema={Yup.object().shape({
            lokasjon: Yup.string().required('Fyll ut lokasjonsnavn'),
        })}
    >
        {props => {
            const {
                values,
                touched,
                errors,
                isSubmitting,
                handleChange,
                handleBlur,
                handleSubmit,
            } = props;

            return (
                <form onSubmit={handleSubmit}>
                    <FormGroup>
                        <label>Legg til lokasjon</label>
                        <Input 
                            id={'lokasjon'}
                            type={'text'}
                            value={values.lokasjon}
                            onChange={handleChange}
                            onBlur={handleBlur}
                            invalid={errors.lokasjon && touched.lokasjon}
                        />
                        <FormFeedback>{errors.lokasjon}</FormFeedback>
                    </FormGroup>
                    <Button 
                        className={'btn btn-success'}
                        disabled={isSubmitting}
                    >
                        Legg til lokasjon
                    </Button>
                </form>
            );
        }}
    </Formik>
)