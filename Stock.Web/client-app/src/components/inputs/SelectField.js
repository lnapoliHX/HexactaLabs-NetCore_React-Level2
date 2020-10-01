import React from "react";
import PropTypes from "prop-types";
import { Input, Label, FormFeedback, FormGroup } from "reactstrap";

const SelectField = props => {
  const {
    multiple,
    input,
    label,
    type,
    options,
    meta: { touched, error, pristine }
  } = props;

  //console.log('selectField value:', value);
  //console.log('props', props);
  //input.value = value;
  //console.log('input:', input);

  return (
    <FormGroup>
      <Label htmlFor={input.name}>{label}</Label>
      <Input
        {...input}
        valid={touched && !error && !pristine}
        invalid={touched && !!error}
        name={input.name}
        id={input.name}
        type={type || "select"}
        multiple={multiple}
      >
        {options.map(each => 
                (<option key={each.id} {...each}>
                  {each.label}
                </option>)
             )}
      </Input>
      <FormFeedback>{error}</FormFeedback>
    </FormGroup>
  );
};

SelectField.propTypes = {
  multiple: PropTypes.bool,
  input: PropTypes.object,
  label: PropTypes.string,
  type: PropTypes.string,
  options: PropTypes.array,
  meta: PropTypes.object
};

export default SelectField;
