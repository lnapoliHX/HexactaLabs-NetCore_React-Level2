import React from "react";
import PropTypes from "prop-types";
import { Field, reduxForm } from "redux-form";
import { Form, Button} from "reactstrap";
//IDEA: Quería agregar un combobox para mostrar las categorías!
import { Dropdown, DropdownMenu, DropdownItem } from "reactstrap";
import Validator from "../../../../common/helpers/YupValidator";
import InputField from "../../../../components/inputs/InputField";
import schema from "../validation";

const ProductForm = props => {
  const { handleSubmit, handleCancel } = props;
  return (
    <Form onSubmit={handleSubmit} className="addForm">
      <Field label="Nombre del producto" name="name" component={InputField} type="text" />
      <Field label="Costo" name="costPrice" component={InputField} type="text" />
      <Field label="Precio de venta" name="salePrice" component={InputField} type="text" />
      <Field label="ID de la categoria" name="productTypeId" component={InputField} type="text" />
      <Field label="Descripcion de la categoria" name="productTypeDesc" component={InputField} type="text" />

     <Dropdown>
      <DropdownMenu>
        <DropdownItem>dropDownValue="ID1"</DropdownItem>
        <DropdownItem>dropDownValue="ID2"</DropdownItem>
        <DropdownItem>dropDownValue="ID3"</DropdownItem>
     </DropdownMenu>
    </Dropdown>
    

      
      <Button className="provider-form__button" color="primary" type="submit">
        Guardar
      </Button>
      <Button
        className="provider-form__button"
        color="secondary"
        type="button"
        onClick={handleCancel}
      >
        Cancelar
      </Button>
    </Form>
  );
};

ProductForm.propTypes = {
  handleSubmit: PropTypes.func.isRequired,
  handleCancel: PropTypes.func.isRequired
};

export default reduxForm({
  form: "product",
  validate: Validator(schema),
  enableReinitialize: true
})(ProductForm);
